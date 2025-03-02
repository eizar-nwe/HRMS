
using System.Net.Sockets;
using System.Net;

namespace HRMS.Web.Utilities
{
    public static class NetworkHelper
    {
        public static async Task<string> GetIpAddressAsync()
        {
            string ip = "unknow";
            string url = "https://ipinfo.io/ip"; //"https://api.ipify.org";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //getting public Ip
                    ip = await client.GetStringAsync(url); //concurrent programming style methods
                }
            }catch (Exception e)
            {
                ip=GetLocalIPAddress(); //getting local Ip
            }
            return ip;
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
