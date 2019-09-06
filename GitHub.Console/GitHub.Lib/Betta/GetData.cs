using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace GitHub.Lib
{
    public class GetData
    {
        public static void GetWebData()
        {
            WebClient source = new WebClient();
            string content = "";
            List<UserInfo> users = new List<UserInfo>();
            for (int i = 0; i < 9; i++)
            {
                content = source.DownloadString("https://api.github.com//search//users?q=location:armenia&page=" + i + "&per_page=100");
                var model1 = JsonConvert.DeserializeObject<TotalInfo>(content);
                users.AddRange(model1.items);
            }

            List<UserExtended> profiles = new List<UserExtended>();
            List<RepoInfo> repos = new List<RepoInfo>();

            for (int i = 0; i < 9; i++)
            {
                string urls = source.DownloadString(users[i].url);
                profiles = JsonConvert.DeserializeObject<List<UserExtended>>(urls);
                string repoUrls = source.DownloadString(users[i].repos_url);
                repos = JsonConvert.DeserializeObject<List<RepoInfo>>(repoUrls);
            }
            

        }

    }
}
