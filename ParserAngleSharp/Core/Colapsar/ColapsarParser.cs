using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserAngleSharp.Core.Colapsar
{
    class ColapsarParser : IParser<Present>
    {
        public Present ParseElement(IHtmlDocument document)
        {
            var name = document.GetElementsByClassName("product-card__header")[0].
                GetElementsByTagName("h1")[0].TextContent;

            var str_price = document.GetElementsByClassName("price price_base")[0].GetElementsByTagName("span")[0].TextContent;
            var price = Int32.Parse(str_price);

            var image = "https://colapsar.ru/" + document.GetElementsByClassName("carousel__item")[0].
                GetElementsByTagName("img")[0].GetAttribute("src");

            var description_list = document.GetElementsByClassName("product-card__section_features")[0].GetElementsByTagName("li");
            var description = "";
            foreach (var item in description_list)
            {
                description += item.TextContent;
                char last_char = item.TextContent[item.TextContent.Length - 1];

                if (last_char != '.')
                {
                    description += ".";
                }
                description += " ";

                if (description.Contains("\n"))
                {
                    description.Replace("\n", "");
                }
            }

            return new Present
            {
                Name = name,
                Price = price,
                Image = image,
                Description = description
            };
        }
        public List<string> GetElementsPages(IHtmlDocument document)
        {
            var items = document.GetElementsByTagName("li").Where(item => item.ClassName != null && item.ClassName.Contains("list__item") &&
            item.ClassList.Contains("ecommerce_productdata"));
            var presents_pages = new List<IHtmlDocument>();
            var result = new List<string>();
            foreach (var item in items)
            {
                var element_page = "https://colapsar.ru" + item.GetElementsByClassName("card__image")[0].GetAttribute("href");
                result.Add(element_page);
            }
            return result;
        }
    }
}
