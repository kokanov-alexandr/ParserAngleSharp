using AngleSharp.Html.Dom;
using ParserAngleSharp.Core.Colapsar;
using System.Collections.Generic;

namespace ParserAngleSharp.Core
{
    interface IParser
    {
        List<string> GetElementsPagesPath(IHtmlDocument document);
        Present ParseElement(IHtmlDocument document);
    }
}
