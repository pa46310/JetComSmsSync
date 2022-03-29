using JetComSmsSync.Core.Models;
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
                using var context1 = LogContext.PushProperty("Server", account.Server);
                ct.ThrowIfCancellationRequested();
                var client = new ServiceClient(account);
               
            }
        }
    }
}
