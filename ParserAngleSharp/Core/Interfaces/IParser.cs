using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace ParserAngleSharp.Core
{
    interface IParser
    {
        List<string> GetElementsPagesPath(IHtmlDocument document);
        BoardGame ParseElement(IHtmlDocument document);
    }
}
