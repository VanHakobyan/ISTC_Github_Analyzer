using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHub.Lib.Gamma;

namespace GitHub.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var profiles = GitInfo.Deserialize();
            var allProfiles = GitInfo.GetAllProfiles();
            var allRepos = GitInfo.GetAllRepo();
            int countOfCSharpRepos = GitInfo.GetCSharpRepos();
            var reposSortedByLanguage = GitInfo.SortByLanguages();
            var bestReposByStars = GitInfo.SortByStars();
            var repo2019 = GitInfo.Get2019Repo();

            System.Console.WriteLine(new string('-', 30) + "\nURl Profiles.");

            foreach (Item item in profiles)
            {
                System.Console.WriteLine(item.html_url);
            }

            System.Console.WriteLine(new string('-', 30) + "\nURL Repositorys.");

            foreach (Repository item in allRepos)
            {
                System.Console.WriteLine(item.html_url);
            }

            System.Console.WriteLine(new string('-', 30) + "\nCount C# Repositorys: {0}", countOfCSharpRepos);
            System.Console.WriteLine(new string('-', 30) + "\nRepositorys sorted by languages.");

            foreach (var item in reposSortedByLanguage)
            {
                var q = item as Repository;
                try
                { 
                    foreach (var item1 in (List<Repository>)item)
                    {
                        System.Console.WriteLine(new string(' ',2) + $"Profile name: {item1.owner.login} // Repository URL: {item1.html_url}");
                    }
                }
                catch
                {
                    System.Console.WriteLine(item);
                }
            }

            System.Console.WriteLine(new string('-', 30) + "\nBest Repositorys by stars.");

            foreach (Repository item in bestReposByStars)
            {
                System.Console.WriteLine($"RepoURL: {item.html_url} {new string(' ', 20)} // Stars count: {item.stargazers_count}");
            }

            System.Console.WriteLine(new string('-', 30) + "\nURL Repositorys created at 2019.");

            foreach (string item in repo2019)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
