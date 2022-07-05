using Newtonsoft.Json;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JetComSmsSync.Modules.loc8nearme.Models.Responses
{
    public class CommentResponse
    {
        public int Id { get; set; }
        [JsonProperty("contributor_id")]
        public int ContributorId { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public string Rating { get; set; }
        [JsonProperty("business_id")]
        public int BusinessId { get; set; }
        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        // additional information for insert
        public int AccountId { get; set; }
        public string RequestURL { get; set; }
    }

    public class CommentComparer : IEqualityComparer<CommentResponse>
    {
        public bool Equals([AllowNull] CommentResponse x, [AllowNull] CommentResponse y)
        {
            if (x is null && y is null) return true;

            if (x is null || y is null) return false;

            return x.Id == y.Id &&
                x.Date == y.Date;
        }

        public int GetHashCode([DisallowNull] CommentResponse item)
        {
            return item.Id.GetHashCode();
        }
    }
}
