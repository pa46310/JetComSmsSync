using JetComSmsSync.Core.Models;
using JetComSmsSync.Core.Utils;
using JetComSmsSync.Modules.TireMasterView.Models;

using Prism.Commands;
using Prism.Mvvm;

using Serilog;
using Serilog.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JetComSmsSync.Modules.TireMasterView.ViewModels
{
    public class TireMasterViewPageViewModel : SyncPageViewModel<AccountModel>
    {
        private readonly DatabaseClient _database;

        protected override ILogger Log => Serilog.Log.ForContext<TireMasterViewPageViewModel>();

        private int _limit = 100;
        public int Limit
        {
            get { return _limit; }
            set { SetProperty(ref _limit, value); }
        }

        public TireMasterViewPageViewModel(DatabaseClient database)
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
            var end = DateTime.Now;
            DateTime start;
            if (startDate.HasValue)
            {
                Log.Debug("Sending delta data");
                start = startDate.Value;
            }
            else
            {
                Log.Debug("Sending bulk data");
                start = DateTime.Today.AddYears(-1);
            }

            foreach (var account in selected)
            {
                current++;
                ct.ThrowIfCancellationRequested();

                Message = $"[{current}/{total}] Getting item counts";
                var service = new ServiceClient(account);
                var count = service.GetItemCount(start);
                using var ctx0 = LogContext.PushProperty("TotalRows", count);
                // live data
                var startForCompare = start.AddDays(-1);
                Message = $"[{current}/{total}] Getting items for compare";

                // customer
                var customerForCompare = _database.GetCustomerForCompare(account.BigId);
                using var ctx1 = LogContext.PushProperty("LiveCustomers", customerForCompare.Count);
                ct.ThrowIfCancellationRequested();
                // vehicle
                var vehicleForCompare = _database.GetVehicleForCompare(account.BigId);
                using var ctx2 = LogContext.PushProperty("LiveVehicles", vehicleForCompare.Count);
                ct.ThrowIfCancellationRequested();
                // ro
                var roForCompare = _database.GetRepairOrderForCompare(account.BigId, startForCompare);
                using var ctx3 = LogContext.PushProperty("LiveRo", roForCompare.Count);
                ct.ThrowIfCancellationRequested();
                // line items
                var lineItemsForCompare = _database.GetLineItemForCompare(account.BigId);
                using var ctx4 = LogContext.PushProperty("LiveLineItems", lineItemsForCompare.Count);
                ct.ThrowIfCancellationRequested();

                // local data
                var offset = 0;
                var inserted = 0;
                var limit = Limit;
                do
                {
                    var offsetEnd = Math.Min(count, offset + limit);
                    Message = $"[{current}/{total}] [{offset}-{offsetEnd}/{count}] Getting local data";
                    var local = service.GetItems(start, offset, limit);

                    Message = $"[{current}/{total}] [{offset}-{offsetEnd}/{count}] Comparing local and live data";
                    // compare data
                    var uniqueCustomer = local.Data.Except(customerForCompare, Comparers.Customer).ToList();
                    customerForCompare.AddRange(uniqueCustomer);
                    using var ctx21 = LogContext.PushProperty("UniqueCustomer", uniqueCustomer.Count);

                    var uniqueVehicles = local.Data.Except(vehicleForCompare, Comparers.Vehicle).ToList();
                    vehicleForCompare.AddRange(uniqueVehicles);
                    using var ctx22 = LogContext.PushProperty("UniqueVehicle", uniqueVehicles.Count);

                    var uniqueRo = local.Data.Except(roForCompare, Comparers.RepairOrder).ToList();
                    roForCompare.AddRange(uniqueRo);
                    using var ctx23 = LogContext.PushProperty("UniqueRo", uniqueRo.Count);

                    var uniqueItems = local.Data.Except(lineItemsForCompare, Comparers.LineItem).ToList();
                    lineItemsForCompare.AddRange(uniqueItems);
                    using var ctx24 = LogContext.PushProperty("UniqueLineItems", uniqueItems.Count);

                    // upload unique data
                    Message = $"[{current}/{total}] [{offset}-{offset + limit}/{count}] Inserting unique data";

                    inserted = _database.InsertCustomer(uniqueCustomer);
                    ct.ThrowIfCancellationRequested();
                    using var ctx11 = LogContext.PushProperty("CustomerInserted", inserted);

                    inserted = _database.InsertVehicles(uniqueVehicles);
                    ct.ThrowIfCancellationRequested();
                    using var ctx12 = LogContext.PushProperty("VehicleInserted", inserted);

                    inserted = _database.InsertRepairOrders(uniqueRo);
                    ct.ThrowIfCancellationRequested();
                    using var ctx13 = LogContext.PushProperty("RepairOrderInserted", inserted);

                    inserted = _database.InsertLineItems(uniqueItems);
                    ct.ThrowIfCancellationRequested();
                    using var ctx14 = LogContext.PushProperty("LineItemInserted", inserted);

                    Message = $"[{current}/{total}] [{offset}-{offset + limit}/{count}] Inserted";
                    offset = offsetEnd;
                    ct.ThrowIfCancellationRequested();
                } while (offset < count);
            }
        }
    }
}
