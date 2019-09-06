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
        static List<Item> profiles = new List<Item>();

        public static void Deserialize()
        {
            for (int i = 1; i < 10; i++)
            {
                string data = SendGetRequest("https://api.github.com/search/users?q=location:armenia&page={i}&per_page=100");
                var persons = JsonConvert.DeserializeObject<Rootobject>(data);
                profiles.AddRange(persons.items);
            }
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
                catch (Exception e)
                {
                    //
                }
            }
            return allGithubContentJson;
        }
    }
}
