using AngleSharp.Html.Parser;
using ParserAngleSharp.Core.Colapsar;
using System;
using System.Collections.Generic;

namespace ParserAngleSharp.Core
{
    class ParserWorker
    {
        IParser parser;
        IParserSettings parserSettings;
        HtmlLoader loader;
        public event Action<object, List<Present>> OnNewData;
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

        public void Start()
        {
            Worker();
        }

        private async void Worker()
        {
            List<string> elements_string_pages = new List<string>();
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                string currentUrl = $"{parserSettings.BaseUrl}/{parserSettings.Prefix}".Replace("{CurrentId}", i.ToString());
                var source = await loader.GetSourseByPath(currentUrl);
                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(source);
                parser.GetElementsPages(document).ForEach(item => elements_string_pages.Add(item));
            }

            var presents = new List<Present>();
            foreach (var item in elements_string_pages)
            {
                var source2 = await loader.GetSourseByPath(item);
                var domParser2 = new HtmlParser();
                var document2 = await domParser2.ParseDocumentAsync(source2);
                presents.Add(parser.ParseElement(document2));

            }
            OnNewData?.Invoke(this, presents);

        }
    }
}
