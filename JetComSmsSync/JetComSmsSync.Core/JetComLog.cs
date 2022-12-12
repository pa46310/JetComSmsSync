using JetComSmsSync.Core.Utils;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Net.NetworkInformation;

namespace JetComSmsSync.Core
{
    public static class JetComLog
    {
        public static RestClient Client { get; }
        static JetComLog()
        {
            Client = RestClientUtils.CreateRestClient("http://jetcomapi.valueaddedonline.com");
            Client.Authenticator = new HttpBasicAuthenticator("admin", "Admin@123");
        }

        public static void Error(string errMsg, string applicationName, string accountId, bool autoSend)
        {
            var input = new
            {
                ApplicationName = applicationName,
                ErrorMessage = errMsg,
                AccountId = accountId,
                IP = GetLocalIPAddress(),
                AutoSend = autoSend,
                Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                MAcId = GetMACAddress()
            };

            var request = new RestRequest("/api/jetcomdatatransfer/InsertErrorLogIntoTable");
            request.AddJsonBody(input);

            var response = Client.Post(request);
            if (!response.IsSuccessful)
            {
                Serilog.Log.Warning(response.Content);
            }
        }

        public static string GetLocalIPAddress()
        {
            try
            {
                string myHost = System.Net.Dns.GetHostName();
                string myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();
                return myIP;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
    }
}
