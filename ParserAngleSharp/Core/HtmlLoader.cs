using System.Net.Http;
using System.Threading.Tasks;

namespace ParserAngleSharp.Core
{
    class HtmlLoader
    {
        readonly HttpClient client;
        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
        }
        public async Task<string> GetSourseByPath(string currentUrl)
        {
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
