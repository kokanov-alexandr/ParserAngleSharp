using AngleSharp.Html.Dom;
using ParserAngleSharp.Core.Colapsar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAngleSharp.Core
{
    interface IParser<T> where T : class
    {
        List<string> GetElementsPages(IHtmlDocument document);
        T ParseElement(IHtmlDocument document);
    }
}
