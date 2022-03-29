using Dapper;
using JetComSmsSync.Modules.TireMasterView.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JetComSmsSync.Modules.TireMasterView
{
    public class DatabaseClient
    {
        public AccountModel[] GetAccounts()
        {
            return new AccountModel[]
            {
                new AccountModel
                {
                    Server = "40.90.236.174",
                    Username = "AAAUser",
                    Password = "TripleAAAAlltheway$",
                }
            };
        }

        private SqlConnection GetConnection(AccountModel account)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = account.Server,
                UserID = account.Username,
                Password = account.Password,
            };

            return new SqlConnection(builder.ConnectionString);
        }

        /// <summary>
        /// Get bulk items
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public IEnumerable<ItemModel> GetItems(AccountModel account)
        {
            using (var connection = GetConnection(account))
            {
                var output = connection.Query<ItemModel>("SELECT * FROM [dbo].[AAAIntegration] ORDER BY OrderId ASC");
                return output;
            }
        }

        public IEnumerable<ItemModel> GetItems(AccountModel account, DateTime startDate)
        {
            using (var connection = GetConnection(account))
            {
                var output = connection.Query<ItemModel>("SELECT * FROM [dbo].[AAAIntegration] WHERE DateOfService>@StartDate ORDER BY OrderId ASC", new { StartDate = startDate });
                return output;
            }
        }
    }
}
