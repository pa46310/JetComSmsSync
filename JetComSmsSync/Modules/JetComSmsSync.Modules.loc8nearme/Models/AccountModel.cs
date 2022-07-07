using JetComSmsSync.Services.Interfaces;

using System.Text.RegularExpressions;

namespace JetComSmsSync.Modules.loc8nearme.Models
{
    public class AccountModel : ISelectable
    {
        public string Id { get; set; }
        public bool IsSelected { get; set; } = true;
        public string BusinessId
        {
            get
            {
                if (string.IsNullOrEmpty(Url)) return null;

                var match = Regex.Match(Url.ToLowerInvariant(), @"/(\d+)/comment");
                if (match.Success && match.Groups.Count > 1)
                {
                    return match.Groups[1].Value;
                }
                else
                {
                    return null;
                }
            }
        }
        public string Url { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
    }
}
