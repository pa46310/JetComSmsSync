using System.Collections.Generic;
using System.Text;

namespace JetComSMSSync.Modules.ShopWare.Responses
{
    public class JsonPageableResponse : PageableResponse<RestSharp.JsonObject>
    {

    }

    public class JsonPaymentContentResponse
    {
        public long Id { get; set; }
        public string Number { get; set; }

        public RestSharp.JsonObject[] Payments { get; set; }
    }

    public class PageableResponse<T>
    {
        public T[] Results { get; set; }
        public int Limit { get; set; }
        public bool Limited { get; set; }
        public long Total_Count { get; set; }
        public long Current_Page { get; set; }
        public long Total_Pages { get; set; }
    }
}
