using Dapper;

using JetComSmsSync.Modules.loc8nearme.Models;
using JetComSmsSync.Modules.loc8nearme.Models.Responses;

using Microsoft.Extensions.Configuration;

using Serilog;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetComSmsSync.Modules.loc8nearme
{
    public class DatabaseClient
    {
        private readonly string _csi03ConnectionString;
        private readonly string _reportsConnectionString;

        public DatabaseClient(IConfiguration configuration)
        {
            _reportsConnectionString = configuration.GetConnectionString("V2Reports");
            _csi03ConnectionString = configuration.GetConnectionString("CSI03");
        }

        public async Task<AccountModel[]> GetAccountsAsync()
        {
            using var connection = new SqlConnection(_reportsConnectionString);
            var accounts = await connection.QueryAsync<AccountModel>(@"SELECT Url, AccId AS AccountId, CONCAT (A.AccountFullName, ', ', a.AccountAddress1, ', ', a.AccountCity, ', ', a.AccountState) AS AccountName
FROM [AcnmSMLoginInfo] l
LEFT JOIN [Accounts] a ON a.AccountId = l.AccID
WHERE SMSite = 'loc8nearme'");
            return accounts.ToArray();
        }

        public int InsertComments(IEnumerable<CommentResponse> comments, int accountId, string requestUrl)
        {
            if (comments is null || !comments.Any()) return 0;

            foreach (var comment in comments)
            {
                comment.AccountId = accountId;
                comment.RequestURL = requestUrl;
            }

            using var connection = new SqlConnection(_csi03ConnectionString);
            var inserted = connection.Execute(@"INSERT INTO [dbo].[Loc8NearMeScrapingData]
           ([RequestURL]
           ,[ID]
           ,[ContributorId]
           ,[Author]
           ,[Date]
           ,[Text]
           ,[Rating]
           ,[BusinessId]
           ,[SiteId]
           ,[AccountID])
     VALUES
           (@RequestURL
           ,@Id
           ,@ContributorId
           ,@Author
           ,@Date
           ,@Text
           ,@Rating
           ,@BusinessId
           ,@SiteId
           ,@AccountID)", comments);
            return inserted;
        }

        public IEnumerable<CommentResponse> GetComments(AccountModel account)
        {

            using var connection = new SqlConnection(_csi03ConnectionString);
            var output = connection.Query<CommentResponse>("SELECT * FROM [Loc8NearMeScrapingData] WHERE AccountID=@AccountId", account);
            return output;
        }
    }
}
