using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAngleSharp.Core.Colapsar
{
    class ColapsarParser : IParser<Present[]>
    {
        public Present[] Parse(IHtmlDocument document)
        {
            var items = document.GetElementsByTagName("li").Where(item => item.ClassName != null && item.ClassName.Contains("list__item") &&
            item.ClassList.Contains("ecommerce_productdata"));
            var result = new List<Present>();
            foreach (var item in items)
            {
                var name = item.GetElementsByClassName("card__title")[0].TextContent.Trim();
                var str_price = item.GetElementsByClassName("price price_base")[0].TextContent.Trim().Replace(" ", "");
                str_price = str_price.Substring(0, str_price.Length - 1);
                var price = Int32.Parse(str_price);
                var image = "https://colapsar.ru/" + item.GetElementsByTagName("img")[0].GetAttribute("src");
                var description_page = "https://colapsar.ru" + item.GetElementsByClassName("card__image")[0].GetAttribute("href");
                result.Add(
                    new Present
                    {
                        Name = name,
                        Price = price,
                        Image = image
                    }
                );
            }
            return result.ToArray();
        }
    }
}
