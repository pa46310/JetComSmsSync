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

        private int _lookBackDays = 10;
        public int LookBackDays
        {
            get { return _lookBackDays; }
            set { SetProperty(ref _lookBackDays, value); }
        }

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
                // live data

                var startForCompare = start.AddDays(-1);
                Message = $"[{current}/{total}] Getting items for compare";
                var customerForCompare = _database.GetCustomerForCompare(account.BigId);
                var vehicleForCompare = _database.GetVehicleForCompare(account.BigId);
                var roForCompare = _database.GetRepairOrderForCompare(account.BigId, startForCompare);
                var lineItemsForCompare = _database.GetLineItemForCompare(account.BigId);

                // local data
                var offset = 0;
                var inserted = 0;
                var limit = Limit;
                foreach (var range in DateUtils.GetRangeByDay(start, end))
                {
                    do
                    {
                        var offsetEnd = Math.Min(count, offset + limit);
                        Message = $"[{current}/{total}] [{offset}-{offsetEnd}/{count}] Getting local data";
                        var local = service.GetItems(range.Item1, offset, limit);

                        Message = $"[{current}/{total}] [{offset}-{offsetEnd}/{count}] Comparing local and live data";
                        // compare data
                        var uniqueCustomer = local.Data.Except(customerForCompare, Comparers.Customer).ToList();
                        customerForCompare.AddRange(uniqueCustomer);

                        var uniqueVehicles = local.Data.Except(vehicleForCompare, Comparers.Vehicle).ToList();
                        vehicleForCompare.AddRange(uniqueVehicles);

                        var uniqueRo = local.Data.Except(roForCompare, Comparers.RepairOrder).ToList();
                        roForCompare.AddRange(uniqueRo);

                        var uniqueItems = local.Data.Except(lineItemsForCompare, Comparers.LineItem).ToList();
                        lineItemsForCompare.AddRange(uniqueItems);

                        // upload unique data
                        Message = $"[{current}/{total}] [{offset}-{offset + limit}/{count}] Inserting unique data";
                        inserted = _database.InsertCustomer(uniqueCustomer);
                        inserted = _database.InsertVehicles(uniqueVehicles);
                        inserted = _database.InsertRepairOrders(uniqueRo);
                        inserted = _database.InsertLineItems(uniqueItems);

                        offset += limit;

                    } while (offset < count);
                }

            }
        }
    }
}
