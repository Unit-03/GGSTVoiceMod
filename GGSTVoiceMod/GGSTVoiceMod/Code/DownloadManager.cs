using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Octokit;

namespace GGSTVoiceMod
{
    public static class DownloadManager
    {
        #region Constants

        // I'll be honest I don't understand web stuff, apparently I need this to download things though so here it is!!
        private const string USER_AGENT = "Mozilla/5.0 (Windows; U; Windows NT 6.1; rv:2.2) Gecko/20110201";

        // head...
        private const string METHOD_HEAD = "HEAD"; // This makes it download the header data
        private const string METHOD_GET  = "GET";  // This makes it download the payload data

        #endregion

        #region Methods

        public static async Task<(bool, string)> HasNewRelease()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue(Paths.RepoName), 
                                                   new Uri($"{Paths.GitHubURL}/{Paths.GitHubUser}"));

            var releases = await client.Repository.Release.GetAll(Paths.GitHubUser, Paths.RepoName);

            if (releases.Count == 0)
                return (false, null);

            try
            {
                string version = releases[0].TagName;
                string[] verParts = version.Split('.');

                byte major = byte.Parse(verParts[0]);
                byte minor = byte.Parse(verParts[1]);

                bool isNewer = major > Constants.MAJOR_VER || (major == Constants.MAJOR_VER && minor > Constants.MINOR_VER);

                return (isNewer, version);
            }
            catch
            {
                return (false, null);
            }
        }

        public static async Task<Stream> DownloadAsset(string charId, string langId)
        {
            Paths.CharacterID = charId;
            Paths.LanguageID  = langId;

            bool cached = File.Exists(Paths.AssetCache);

            if (!cached && Settings.UseCache == true)
            {
                string cacheDir = Path.GetDirectoryName(Paths.AssetCache);

                if (!Directory.Exists(cacheDir))
                    Directory.CreateDirectory(cacheDir);

                using WebClient client = new WebClient();
                await client.DownloadFileTaskAsync(new Uri(Paths.AssetDownloadURL), Paths.AssetCache);
                cached = true;
            }

            if (cached)
                return File.OpenRead(Paths.AssetCache);

            HttpWebRequest request = WebRequest.CreateHttp(Paths.AssetDownloadURL);
            request.UserAgent = USER_AGENT;
            request.Method    = METHOD_GET;

            using WebResponse response = await request.GetResponseAsync();
            return response.GetResponseStream();
        }

        public static async Task<WebResponse> GetHeaderData(string charId, string langId)
        {
            Paths.CharacterID = charId;
            Paths.LanguageID  = langId;

            HttpWebRequest request = WebRequest.CreateHttp(Paths.AssetDownloadURL);
            request.UserAgent = USER_AGENT;
            request.Method    = METHOD_HEAD;

            return await request.GetResponseAsync();
        }

        public static async Task<long> GetDownloadSize(string charId, string langId)
        {
            Paths.CharacterID = charId;
            Paths.LanguageID  = langId;

            if (File.Exists(Paths.AssetCache))
                return 0;

            HttpWebRequest request = WebRequest.CreateHttp(Paths.AssetDownloadURL);
            request.UserAgent = USER_AGENT;
            request.Method    = METHOD_HEAD;

            using WebResponse response = await request.GetResponseAsync();
            return response.ContentLength;
        }

        public static async Task<long> GetDownloadSize(PatchInfo patchInfo)
        {
            long totalSize = 0;

            foreach (var patch in patchInfo)
                totalSize += await GetDownloadSize(patch.Character, patch.UseLang);

            return totalSize;
        }

        #endregion
    }
}
