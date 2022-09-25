using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

using Octokit;

namespace UpdateAgent
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
                return;
            
            UpdateToVersion(args[0], args[1]).Wait();
        }

        private static async Task UpdateToVersion(string processId, string version)
        {
            Process parent = Process.GetProcessById(int.Parse(processId));
            parent.WaitForExit();

            string root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            GitHubClient client = new GitHubClient(new ProductHeaderValue("GGSTVoiceMod"),
                                                   new Uri("https://github.com/Unit-03"));

            var releases = await client.Repository.Release.GetAll("Unit-03", "GGSTVoiceMod");

            foreach (var release in releases)
            {
                if (release.TagName == version)
                {
                    foreach (var asset in release.Assets)
                    {
                        if (asset.Name == "GGSTVoiceMod.zip")
                        {
                            HttpWebRequest request = WebRequest.CreateHttp(asset.BrowserDownloadUrl);
                            request.Method = "GET";

                            using WebResponse response = await request.GetResponseAsync();
                            using ZipArchive zip = new ZipArchive(response.GetResponseStream());

                            foreach (var entry in zip.Entries)
                            {
                                if (string.IsNullOrWhiteSpace(entry.Name))
                                    continue;

                                try
                                {
                                    string extractPath = Path.Combine(root, entry.FullName);
                                    string extractDir  = Path.GetDirectoryName(extractPath);

                                    if (!Directory.Exists(extractDir))
                                        Directory.CreateDirectory(extractDir);

                                    entry.ExtractToFile(Path.Combine(root, entry.FullName), true);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
            }

            Process.Start(Path.Combine(root, "GGSTVoiceMod.exe"));
        }
    }
}
