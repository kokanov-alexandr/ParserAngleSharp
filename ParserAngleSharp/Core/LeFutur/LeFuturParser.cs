using AngleSharp.Html.Dom;
using ParserAngleSharp.Core.Colapsar;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ParserAngleSharp.Core.MrGeek
{
    internal class LeFuturParser : IParser
    {
        public List<string> GetElementsPages(IHtmlDocument document)
        {
            var items = document.GetElementsByClassName("prod-list-item");
            var result = new List<string>();
            foreach (var item in items)
            {
                var element_page = "https://lefutur.ru/" + item.GetElementsByTagName("a")[0].GetAttribute("href");
                result.Add(element_page);
            }
            return result;
        }

        public Present ParseElement(IHtmlDocument document)
        {

            var name = document.GetElementsByClassName("prod-info")[0].
                GetElementsByTagName("h1")[0].TextContent;
            
            
            var str_price = document.GetElementsByClassName("prod-price-box")[0].GetElementsByTagName("span")[0].TextContent;

            if (str_price.Length == 0)
            {
                str_price = document.GetElementsByClassName("prod-price-box")[0].GetElementsByTagName("font")[0].
                    GetElementsByTagName("font")[0].TextContent;
            }

            str_price = str_price.Replace(((char)(160)).ToString(), "").Split()[0];
                
            var price = Int32.Parse(str_price);
            
            var image = "https://lefutur.ru/" + document.GetElementsByClassName("prod-info")[0].
                GetElementsByTagName("img")[0].GetAttribute("src");


            var description_list = document.GetElementsByClassName("prod-desc");
            if (description_list.Length > 0)
            {
                description_list = description_list[0].GetElementsByTagName("p");
            }
            var description = "";
            if (description_list != null)
            {
                foreach (var item in description_list)
                {
                    description += item.TextContent;
                    if (item.TextContent.Length == 0)
                    {
                        continue;
                    }
                    char last_char = item.TextContent[item.TextContent.Length - 1];

                    if (last_char != '.')
                    {
                        description += ".";
                    }
                    description += " ";

                    if (description.Contains("\n"))
                    {
                        description = description.Replace("\n", "");
                    }
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
    }
}
