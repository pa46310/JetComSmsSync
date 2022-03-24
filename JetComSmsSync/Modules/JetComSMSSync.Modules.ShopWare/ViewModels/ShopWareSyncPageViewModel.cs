using JetComSmsSync.Core.Models;
using JetComSmsSync.Core.Utils;
using JetComSMSSync.Modules.ShopWare.Adapters;
using JetComSMSSync.Modules.ShopWare.Models;
using Prism.Mvvm;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JetComSMSSync.Modules.ShopWare.ViewModels
{
    public class ShopWareSyncPageViewModel : SyncPageViewModel<AccountModel>
    {
        private readonly DatabaseClient _database;

        private int _lookBackDays = 10;
        public int LookBackDays
        {
            get { return _lookBackDays; }
            set { SetProperty(ref _lookBackDays, value); }
        }

        public ShopWareSyncPageViewModel(DatabaseClient database)
        {
            _database = database;
            RefreshCommand.Execute();
        }

        protected override Task<AccountModel[]> GetAccounts()
        {
            return Task.Run(_database.GetAccounts);
        }

        protected override void Send(DateTime? startDate, IList<AccountModel> selected, CancellationToken ct)
        {
            var current = 0;
            var total = selected.Count;
            var start = DateTime.MinValue;
            var end = DateTime.Now;
            if (startDate.HasValue)
            {
                Log.Debug("Sending delta data");
                start = startDate.Value;
            }
            else
            {
                Log.Debug("Sending bulk data");
            }

            foreach (var account in selected)
            {
                current++;
                using var context1 = LogContext.PushProperty("ShopID", account.ShopID);
                using var context2 = LogContext.PushProperty("BigID", account.BigID);
                ct.ThrowIfCancellationRequested();
                var client = new ServiceClient(account);
                string prefix;

                prefix = $"[{current}/{total}] [Customer]";
                SendCustomerData(client, account, prefix, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [PastRecommendation]";
                SendPastRecomendationData(client, account, prefix, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [Payment]";
                SendPaymentsData(client, account, prefix, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [RepairOrder]";
                SendRepairOrderData(client, account, prefix, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [Vehicle]";
                SendVehicleData(client, account, prefix, ct);
                ct.ThrowIfCancellationRequested();
            }
        }

        private void SendVehicleData(ServiceClient client, AccountModel account, string prefix, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetVehicleForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var inserted = 0;

                Message = prefix + " Getting data...";
                foreach (var item in client.GetVehicles(1, LookBackDays))
                {
                    var unique = item.Results.Except(compare, Comparers.Vehicle).ToList();
                    compare.AddRange(unique);
                    inserted += _database.InsertVehicle(unique);
                    Log.Debug("{0} inserted", inserted);
                    ct.ThrowIfCancellationRequested();

                    Message = $"{prefix} Sent: {item.Current_Page}/{item.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }

        private void SendRepairOrderData(ServiceClient client, AccountModel account, string prefix, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compareRepairOrder = _database.GetRepairOrdersForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var compareServices = _database.GetServiceForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var compareLabors = _database.GetServiceLaborsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var compareParts = _database.GetServicePartForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var compareHazmats = _database.GetServiceHazmatsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var compareSublets = _database.GetServiceSubletForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var compareInspection = _database.GetServiceInspectionsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();

                Message = prefix + " Getting data...";
                foreach (var item in client.GetRepairOrders(1, LookBackDays))
                {
                    // repair order
                    var repairOrders = item.Results.Select(x => x.ToRepairOrder(account.BigID));
                    var uniqueRepairOrder = repairOrders.Except(compareRepairOrder, Comparers.RepairOrder).ToList();
                    compareRepairOrder.AddRange(uniqueRepairOrder);
                    var inserted = _database.InsertRepairOrders(uniqueRepairOrder);
                    ct.ThrowIfCancellationRequested();
                    // payments (not required since it is done in another send)
                    // services
                    var services = item.Results.SelectMany(x => x.ToServices(account.BigID));
                    var uniqueServices = services.Except(compareServices, Comparers.Service).ToList();
                    compareServices.AddRange(uniqueServices);
                    inserted = _database.InsertServices(uniqueServices);
                    ct.ThrowIfCancellationRequested();
                    // labors
                    var labors = item.Results.SelectMany(x => x.ToLabors(account.BigID));
                    var uniqueLabors = labors.Except(compareLabors, Comparers.ServiceLabor).ToList();
                    compareLabors.AddRange(uniqueLabors);
                    inserted = _database.InsertServiceLabors(uniqueLabors);
                    ct.ThrowIfCancellationRequested();
                    // parts
                    var parts = item.Results.SelectMany(x => x.ToParts(account.BigID));
                    var uniqueParts = parts.Except(compareParts, Comparers.ServicePart).ToList();
                    compareParts.AddRange(uniqueParts);
                    inserted = _database.InsertServiceParts(uniqueParts);
                    ct.ThrowIfCancellationRequested();
                    // hazmats
                    var hazmats = item.Results.SelectMany(x => x.ToHazmats(account.BigID));
                    var uniqueHazmats = hazmats.Except(compareHazmats, Comparers.ServiceHazmat).ToList();
                    compareHazmats.AddRange(uniqueHazmats);
                    inserted = _database.InsertServiceHazmats(uniqueHazmats);
                    ct.ThrowIfCancellationRequested();
                    // sublet
                    var sublet = item.Results.SelectMany(x => x.ToSublets(account.BigID));
                    var uniqueSublet = sublet.Except(compareSublets, Comparers.ServiceSublet).ToList();
                    compareSublets.AddRange(uniqueSublet);
                    inserted = _database.InsertServiceSublets(uniqueSublet);
                    ct.ThrowIfCancellationRequested();
                    // inspection
                    var inspections = item.Results.SelectMany(x => x.ToInspections(account.BigID));
                    var uniqueInspections = inspections.Except(compareInspection, Comparers.ServiceInspection).ToList();
                    compareInspection.AddRange(uniqueInspections);
                    inserted = _database.InsertServiceInspections(uniqueInspections);
                    ct.ThrowIfCancellationRequested();

                    Message = $"{prefix} Sent: {item.Current_Page}/{item.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }

        private void SendPaymentsData(ServiceClient client, AccountModel account, string prefix, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetPaymentsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var inserted = 0;

                Message = prefix + " Getting data...";
                foreach (var item in client.GetPayments(1, LookBackDays))
                {
                    var unique = item.Results.Except(compare, Comparers.Payment).ToList();
                    compare.AddRange(unique);
                    inserted += _database.InsertPayments(unique);
                    Log.Debug("{0} inserted", inserted);
                    ct.ThrowIfCancellationRequested();

                    Message = $"{prefix} Sent: {item.Current_Page}/{item.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }

        private void SendPastRecomendationData(ServiceClient client, AccountModel account, string prefix, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetPastRecommendationsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                var inserted = 0;

                Message = prefix + " Getting data...";
                foreach (var item in client.GetPastRecomendations(1, LookBackDays))
                {
                    var unique = item.Results.Except(compare, Comparers.PastRecomendation).ToList();
                    compare.AddRange(unique);
                    inserted += _database.InsertPastRecomendations(unique);
                    Log.Debug("{0} inserted", inserted);
                    ct.ThrowIfCancellationRequested();

                    Message = $"{prefix} Sent: {item.Current_Page}/{item.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }

        private void SendCustomerData(ServiceClient client, AccountModel account, string prefix, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var uniqueCustomers = _database.GetCustomersForCompare(account.BigID, account.ShopID);
                ct.ThrowIfCancellationRequested();
                var inserted = 0;

                Message = prefix + " Getting data...";
                foreach (var customers in client.GetCustomers(1, LookBackDays))
                {
                    var unique = customers.Results.Except(uniqueCustomers, Comparers.Customer).ToList();
                    uniqueCustomers.AddRange(unique);
                    inserted += _database.InsertCustomers(unique);
                    Log.Debug("{0} inserted", inserted);
                    ct.ThrowIfCancellationRequested();

                    Message = $"{prefix} Sent: {customers.Current_Page}/{customers.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }
    }
}
