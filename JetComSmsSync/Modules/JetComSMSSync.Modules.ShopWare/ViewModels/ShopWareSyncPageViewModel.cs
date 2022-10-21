using JetComSmsSync.Core;
using JetComSmsSync.Core.Models;

using JetComSMSSync.Modules.ShopWare.Adapters;
using JetComSMSSync.Modules.ShopWare.Models;

using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Services.Dialogs;

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
        private readonly IDialogService _dialog;

        protected override ILogger Log => Serilog.Log.ForContext<ShopWareSyncPageViewModel>();

        public ShopWareSyncPageViewModel(DatabaseClient database, IDialogService dialog)
        {
            _database = database;
            _dialog = dialog;
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

            foreach (var account in selected)
            {
                current++;
                using var context1 = LogContext.PushProperty("ShopID", account.ShopID);
                using var context2 = LogContext.PushProperty("BigID", account.BigID);
                ct.ThrowIfCancellationRequested();
                var client = new ServiceClient(account);
                string prefix;

                prefix = $"[{current}/{total}] [Customer]";
                SendCustomerData(client, account, prefix, startDate, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [PastRecommendation]";
                SendPastRecomendationData(client, account, prefix, startDate, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [Payment]";
                SendPaymentsData(client, account, prefix, startDate, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [RepairOrder]";
                SendRepairOrderData(client, account, prefix, startDate, ct);
                ct.ThrowIfCancellationRequested();

                prefix = $"[{current}/{total}] [Vehicle]";
                SendVehicleData(client, account, prefix, startDate, ct);
                ct.ThrowIfCancellationRequested();
            }
        }

        private void SendVehicleData(ServiceClient client, AccountModel account, string prefix, DateTime? start, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetVehicleForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();
                using var context1 = LogContext.PushProperty("Local", compare.Count);

                Message = prefix + " Getting data...";
                foreach (var item in client.GetVehicles(start))
                {
                    using var contextLocal = LogContext.PushProperty("Local", compare.Count);
                    var unique = item.Results.Except(compare, Comparers.Vehicle).ToList();
                    compare.AddRange(unique);
                    using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                    var inserted = _database.InsertVehicle(unique);
                    using var contextInserted = LogContext.PushProperty("Inserted", inserted);
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

        private void SendRepairOrderData(ServiceClient client, AccountModel account, string prefix, DateTime? start, CancellationToken ct)
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
                foreach (var item in client.GetRepairOrders(start))
                {
                    #region Repair orders
                    using (var contextLocal = LogContext.PushProperty("Local", compareRepairOrder.Count))
                    {
                        // unique
                        var items = item.Results.Select(x => x.ToRepairOrder(account.BigID));
                        var uniqueItems = items.Except(compareRepairOrder, Comparers.RepairOrder).ToList();
                        compareRepairOrder.AddRange(uniqueItems);
                        using var contextUnique = LogContext.PushProperty("Unique", uniqueItems.Count);

                        // insert
                        var inserted = _database.InsertRepairOrders(uniqueItems);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} repair order inserted", inserted);
                        ct.ThrowIfCancellationRequested();

                        // update
                        var toUpdate = items.Intersect(compareRepairOrder, Comparers.RepairOrderUpdate).ToList();
                        var updated = _database.UpdateRepairOrders(toUpdate);
                        using var contextUpdated = LogContext.PushProperty("Updated", updated);
                        Log.Debug("{0} repair order updated", updated);
                    }
                    #endregion

                    #region Payments (not required since it is done in another send) 

                    #endregion

                    #region Services
                    using (var contextLocal = LogContext.PushProperty("Local", compareServices.Count))
                    {
                        // unique
                        var items = item.Results.SelectMany(x => x.ToServices(account.BigID));
                        var unique = items.Except(compareServices, Comparers.Service).ToList();
                        compareServices.AddRange(unique);
                        using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                        // insert
                        var inserted = _database.InsertServices(unique);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} service inserted", inserted);
                        ct.ThrowIfCancellationRequested();
                    }
                    #endregion

                    #region Labors
                    using (var contextLocal = LogContext.PushProperty("Local", compareLabors.Count))
                    {
                        // unique
                        var items = item.Results.SelectMany(x => x.ToLabors(account.BigID));
                        var unique = items.Except(compareLabors, Comparers.ServiceLabor).ToList();
                        compareLabors.AddRange(unique);
                        using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                        // insert
                        var inserted = _database.InsertServiceLabors(unique);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} labor inserted", inserted);
                        ct.ThrowIfCancellationRequested();
                    }
                    #endregion

                    #region Parts
                    using (var contextLocal = LogContext.PushProperty("Local", compareParts.Count))
                    {
                        // unique
                        var items = item.Results.SelectMany(x => x.ToParts(account.BigID));
                        var unique = items.Except(compareParts, Comparers.ServicePart).ToList();
                        compareParts.AddRange(unique);
                        using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                        // insert
                        var inserted = _database.InsertServiceParts(unique);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} part inserted", inserted);
                        ct.ThrowIfCancellationRequested();
                    }
                    #endregion

                    #region Hazmats
                    using (var contextLocal = LogContext.PushProperty("Local", compareHazmats.Count))
                    {
                        // unique
                        var items = item.Results.SelectMany(x => x.ToHazmats(account.BigID));
                        var unique = items.Except(compareHazmats, Comparers.ServiceHazmat).ToList();
                        compareHazmats.AddRange(unique);
                        using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                        // insert
                        var inserted = _database.InsertServiceHazmats(unique);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} hazmat inserted", inserted);
                        ct.ThrowIfCancellationRequested();
                    }
                    #endregion

                    #region Sublet
                    using (var contextLocal = LogContext.PushProperty("Local", compareSublets.Count))
                    {
                        var items = item.Results.SelectMany(x => x.ToSublets(account.BigID));
                        var unique = items.Except(compareSublets, Comparers.ServiceSublet).ToList();
                        compareSublets.AddRange(unique);
                        using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                        // insert
                        var inserted = _database.InsertServiceSublets(unique);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} sublet inserted", inserted);
                        ct.ThrowIfCancellationRequested();
                    }
                    #endregion

                    #region Inspection
                    using (var contextLocal = LogContext.PushProperty("Local", compareInspection.Count))
                    {
                        // unique
                        var items = item.Results.SelectMany(x => x.ToInspections(account.BigID));
                        var unique = items.Except(compareInspection, Comparers.ServiceInspection).ToList();
                        compareInspection.AddRange(unique);
                        using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                        // insert
                        var inserted = _database.InsertServiceInspections(unique);
                        using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                        Log.Debug("{0} inspection inserted", inserted);
                        ct.ThrowIfCancellationRequested();
                    }
                    #endregion

                    Message = $"{prefix} Sent: {item.Current_Page}/{item.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }

        private void SendPaymentsData(ServiceClient client, AccountModel account, string prefix, DateTime? start, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetPaymentsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();

                Message = prefix + " Getting data...";
                foreach (var item in client.GetPayments(start))
                {
                    using var contextLocal = LogContext.PushProperty("Local", compare.Count);
                    var unique = item.Results.Except(compare, Comparers.Payment).ToList();
                    compare.AddRange(unique);
                    using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                    var inserted = _database.InsertPayments(unique);
                    using var contextInserted = LogContext.PushProperty("Inserted", inserted);
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

        private void SendPastRecomendationData(ServiceClient client, AccountModel account, string prefix, DateTime? start, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetPastRecommendationsForCompare(account.BigID);
                ct.ThrowIfCancellationRequested();

                Message = prefix + " Getting data...";
                foreach (var item in client.GetPastRecomendations(start))
                {
                    using var contextLocal = LogContext.PushProperty("Local", compare.Count);
                    var unique = item.Results.Except(compare, Comparers.PastRecomendation).ToList();
                    compare.AddRange(unique);
                    using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                    var inserted = _database.InsertPastRecomendations(unique);
                    using var contextInserted = LogContext.PushProperty("Inserted", inserted);
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

        private void SendCustomerData(ServiceClient client, AccountModel account, string prefix, DateTime? start, CancellationToken ct)
        {
            try
            {
                Message = prefix + " Getting data for compare";
                var compare = _database.GetCustomersForCompare(account.BigID, account.ShopID);
                ct.ThrowIfCancellationRequested();

                Message = prefix + " Getting data...";
                foreach (var customers in client.GetCustomers(start))
                {
                    #region Insertions
                    using var contextLocal = LogContext.PushProperty("Local", compare.Count);
                    var unique = customers.Results.Except(compare, Comparers.Customer).ToList();
                    compare.AddRange(unique);
                    using var contextUnique = LogContext.PushProperty("Unique", unique.Count);

                    var inserted = _database.InsertCustomers(unique);
                    using var contextInserted = LogContext.PushProperty("Inserted", inserted);
                    Log.Debug("{0} inserted", inserted);
                    ct.ThrowIfCancellationRequested();
                    #endregion

                    #region Update
                    var toUpdate = customers.Results.Intersect(compare, Comparers.CustomerUpdate).ToList();
                    var updated = _database.UpdateCustomers(toUpdate);
                    Log.Debug("{0} updated", updated);
                    #endregion

                    Message = $"{prefix} Sent: {customers.Current_Page}/{customers.Total_Pages}";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send data");
            }
        }

        private DelegateCommand<AccountModel> _listAllShopCommand;
        public DelegateCommand<AccountModel> ListAllShopCommand =>
            _listAllShopCommand ?? (_listAllShopCommand = new DelegateCommand<AccountModel>(ExecuteListAllShopCommand));

        async void ExecuteListAllShopCommand(AccountModel account)
        {
            try
            {
                if (account is null) return;

                MessageService.Instance.ShowPersistentMessage("Getting shops...");

                var client = new ServiceClient(account);
                var pages = await Task.Run(() => client.GetShops().ToList());
                var shops = pages.SelectMany(x => x.Results).ToArray();
                MessageService.Instance.HidePersistentMessage();

                var p = new DialogParameters
                {
                    { "Items", shops }
                };

                _dialog.ShowDialog(nameof(Views.ViewShopsPage), p, res =>
                {
                });
            }
            catch (Exception ex)
            {
                MessageService.Instance.HidePersistentMessage();
                Log.LogAndShowError(ex, "Failed to get shops");
            }
        }
    }
}
