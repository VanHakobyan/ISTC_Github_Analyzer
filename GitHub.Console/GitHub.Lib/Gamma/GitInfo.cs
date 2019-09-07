using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace GitHub.Lib.Gamma//create_at.year
{
    public static class GitInfo
    {
        private static List<Item> profiles = new List<Item>();
        private static List<URL_Rootobject> allPprofiles = new List<URL_Rootobject>();
        private static List<Repository> allRepo = new List<Repository>();

        public static IEnumerable Deserialize()
        {
            for (int i = 1; i < 10; i++)
            {
                string data = SendGetRequest("https://api.github.com/search/users?q=location:armenia&page={i}&per_page=100");
                var persons = JsonConvert.DeserializeObject<Rootobject>(data);
                profiles.AddRange(persons.items);
            }
            return profiles;
        }

        public static IEnumerable GetAllProfiles()
        {
            int i = 0;
            foreach (var item in profiles)
            {
                string data = SendGetRequest(item.url);
                URL_Rootobject persons = JsonConvert.DeserializeObject<URL_Rootobject>(data);
                allPprofiles.Add(persons);

                if (i++ == 15)
                    break;
            }
            return allPprofiles;
        }

        public static IEnumerable GetAllRepo()
        {
            int i = 0;
            foreach (var item in profiles)
            {
                string data = SendGetRequest(item.repos_url);
                List<Repository> repos = JsonConvert.DeserializeObject<List<Repository>>(data);
                allRepo.AddRange(repos);
                if (i++ == 15)
                    break;
            }

            return allRepo;
        }

        public static IEnumerable<Repository> SortByStars()
        {
            return allRepo.OrderBy(x => x.stargazers_count).Reverse();
        }

        public static int GetCSharpRepos()
        {
            return allRepo.Where(x => x.language == "C#").Select(x => x).ToList().Count;
        }

        private static List<string> GetLanguagesList()
        {
            return allRepo.GroupBy(x => x).Select(x => x.Key.language).Distinct().ToList();
        }

        public static IEnumerable SortByLanguages()
        {
            List<string> language = GetLanguagesList();
            foreach (var lang in language)
            {
                List<Repository> repo1 = new List<Repository>();
                foreach (var repository in allRepo)
                {
                    if (repository.language == lang)
                    {
                        repo1.Add(repository);
                    }
                }

                yield return lang;
                yield return repo1;
            }
        }

        public static IEnumerable Get2019Repo()
        {
            return allRepo.Where(x => x.created_at.Year == 2019).Select(x => x.owner.html_url);
        }



        private static string SendGetRequest(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

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
                    Console.WriteLine(e.Message);
                }
            }
            return allGithubContentJson;
        }
    }

}
