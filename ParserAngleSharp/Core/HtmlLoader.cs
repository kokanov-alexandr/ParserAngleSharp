using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParserAngleSharp.Core
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string Url;
        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            Url = $"{settings.BaseUrl}/{settings.Prefix}";
        }

        public async Task<string> GetSourseBypageId(int id)
        {
            var currentUrl = Url.Replace("{CurrentId}", id.ToString());
            var response = await client.GetAsync(currentUrl);
            string sourse = null;

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                sourse = await response.Content.ReadAsStringAsync();
            }

            return sourse;
        }
    }
}
