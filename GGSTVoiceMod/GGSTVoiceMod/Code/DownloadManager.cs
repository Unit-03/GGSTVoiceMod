using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GGSTVoiceMod
{
    public static class DownloadManager
    {
        #region Constants

        private const string USER_AGENT = "Mozilla/5.0 (Windows; U; Windows NT 6.1; rv:2.2) Gecko/20110201";

        // head...
        private const string METHOD_HEAD = "HEAD";
        private const string METHOD_GET  = "GET";

        #endregion

        #region Methods

        public static async Task<WebResponse> GetHeaderData(string charId, string langId)
        {
            Paths.CharacterID = charId;
            Paths.LanguageID  = langId;

            HttpWebRequest request = WebRequest.CreateHttp(Paths.AssetDownloadURL);
            request.UserAgent = USER_AGENT;
            request.Method    = METHOD_HEAD;

            return await request.GetResponseAsync();
        }

        public static async Task<long> GetDownloadSize(PatchInfo patchInfo)
        {
            long totalSize = 0;

            foreach (var patch in patchInfo)
            {
                Paths.CharacterID = patch.Character;
                Paths.LanguageID  = patch.UseLang;

                HttpWebRequest request = WebRequest.CreateHttp(Paths.AssetDownloadURL);
                request.UserAgent = USER_AGENT;
                request.Method    = METHOD_HEAD;

                using WebResponse response = await request.GetResponseAsync();
                totalSize += response.ContentLength;
            }

            return totalSize;
        }

        #endregion
    }
}
