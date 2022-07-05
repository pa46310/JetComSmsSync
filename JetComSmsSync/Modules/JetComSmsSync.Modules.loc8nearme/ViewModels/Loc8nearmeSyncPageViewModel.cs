using JetComSmsSync.Core;
using JetComSmsSync.Core.Models;
using JetComSmsSync.Modules.loc8nearme.Models;
using JetComSmsSync.Modules.loc8nearme.Models.Responses;

using Serilog;
using Serilog.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JetComSmsSync.Modules.loc8nearme.ViewModels
{
    public class Loc8nearmeSyncPageViewModel : SyncPageViewModel<AccountModel>
    {
        private readonly ServiceClient _service;
        private readonly DatabaseClient _database;

        protected override ILogger Log => Serilog.Log.ForContext<Loc8nearmeSyncPageViewModel>();

        public Loc8nearmeSyncPageViewModel(ServiceClient service, DatabaseClient database)
        {
            _service = service;
            _database = database;
        }

        protected override Task<AccountModel[]> GetAccounts()
        {
            return _database.GetAccountsAsync();
        }

        protected override void Send(DateTime? startDate, IList<AccountModel> accounts, CancellationToken ct)
        {
            if (accounts is null || accounts.Count == 0) return;

            try
            {
                var index = 0;
                foreach (var account in accounts)
                {
                    index++;
                    var prefix = $"[{index:N0}/{accounts.Count:N0}]";

                    using var context1 = LogContext.PushProperty("AccountId", account.AccountId);
                    // Get local
                    MessageService.Instance.ShowPersistentMessage($"{prefix} Getting SQL Server data...");
                    var local = _database.GetComments(account).ToList();
                    Log.Debug("Local: {0}", local.Count);

                    // Get server
                    var page = 1;
                    MessageService.Instance.ShowPersistentMessage($"{prefix} Getting Loc8NearMeData data({page++:N0})...");
                    foreach (var comments in _service.GetAllComments(account.BusinessId))
                    {
                        try
                        {
                            Log.Debug("Server: {0}", comments.Length);

                            // Get unique
                            var unique = comments.Except(local, new CommentComparer()).ToList();
                            Log.Debug("Unique: {0}", unique.Count);

                            // send data to database
                            MessageService.Instance.ShowPersistentMessage($"{prefix} Inserting data to SQL Server...");
                            var inserted = _database.InsertComments(unique, account.AccountId, account.Url);
                            Log.Debug("Inserted: {0}", inserted);
                            MessageService.Instance.ShowPersistentMessage($"{prefix}Getting Loc8NearMeData data({page++:N0})...");
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Failed to insert comment to server");
                        }
                    }
                }
                MessageService.Instance.HidePersistentMessage();
                MessageService.Instance.EnqueInformation("Sync successful");
            }
            finally
            {
                MessageService.Instance.HidePersistentMessage();
            }
        }
    }
}
