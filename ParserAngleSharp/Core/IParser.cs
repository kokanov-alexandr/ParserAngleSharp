using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace ParserAngleSharp.Core
{
    interface IParser<T> where T : class
    {
        List<string> GetElementsPages(IHtmlDocument document);
        T ParseElement(IHtmlDocument document);
    }
}
