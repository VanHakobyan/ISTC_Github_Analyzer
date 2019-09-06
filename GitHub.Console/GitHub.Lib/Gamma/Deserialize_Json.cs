using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHub.Lib.Gamma
{
    public static class Ser_Deser_Json
    {
        public static Data Deserialize()
        {
            string data = SendGetRequest("https://api.github.com/search/users?q=location:armenia&page=1&per_page=100");
            return JsonConvert.DeserializeObject<Data>(data) as Data;
        }

        public static string Serialize(Data ob)
        {
            return JsonConvert.SerializeObject(ob);
        }

        public static void AddProfiles()
        {

        }


        private static string SendGetRequest(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;

            WebClient client = new WebClient();

            string allGithubContentJson = null;
            while (true)
            {
                try
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Anything");
                    client.Headers.Add(HttpRequestHeader.ContentType, "applicaton/json");
                    allGithubContentJson = client.DownloadString(url);
                    break;
                }
                catch(Exception e)
                {
                    //
                }
            }
            return allGithubContentJson;
        }
    }
}
