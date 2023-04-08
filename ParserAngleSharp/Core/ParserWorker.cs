using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;

namespace ParserAngleSharp.Core
{
    class ParserWorker
    {
        IParser parser;
        IParserSettings parserSettings;
        HtmlLoader loader;
        public event Action<object, List<BoardGame>> OnNewData;
        public event Action<object, int> OnPasedPage;
        public ParserWorker(IParser parser)
        {
            this.parser = parser;
        }
        public ParserWorker(IParser parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public IParser Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

      
        public async void Worker()
        {
            List<string> elements_string_pages = new List<string>();
            var presents = new List<BoardGame>();
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                string currentUrl = $"{parserSettings.BaseUrl}{parserSettings.Prefix}".Replace("{CurrentId}", i.ToString());
                var source = await loader.GetSourseByPath(currentUrl);
                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(source);
                parser.GetElementsPagesPath(document).ForEach(item => elements_string_pages.Add(item));
                
                presents = new List<BoardGame>();
                foreach (var item in elements_string_pages)
                {
                    var source2 = await loader.GetSourseByPath(item);
                    var domParser2 = new HtmlParser();
                    var document2 = await domParser2.ParseDocumentAsync(source2);
                    var element = parser.ParseElement(document2);
                    if (element != null)
                    {
                        presents.Add(element);
                    }
                }
                OnPasedPage?.Invoke(this, i);
            }
            OnNewData?.Invoke(this, presents);
        }
    }
}
