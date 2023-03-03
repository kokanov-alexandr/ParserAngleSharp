using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ParserAngleSharp.Core.Colapsar;
using ParserAngleSharp.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace ParserAngleSharp.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;
        HtmlLoader loader;
        public event Action<object, List<T>> OnNewData;
        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }
        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public IParser<T> Parser
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

            var presents = new List<T>();
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
