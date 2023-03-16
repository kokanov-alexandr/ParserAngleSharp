using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserAngleSharp.Core.IgroTime
{
    internal class IgroTimeParser : IParser
    {
        public List<string> GetElementsPagesPath(IHtmlDocument document)
        {
            var items = document.GetElementsByClassName("good-card");
            var result = new List<string>();
            foreach (var item in items)
            {
                var element_page = "https://www.igrotime.ru" + item.GetElementsByTagName("a")[0].GetAttribute("href");
                result.Add(element_page);
            }
            return result;
        }

        public BoardGame ParseElement(IHtmlDocument document)
        {
            var name = document.GetElementsByClassName("col-rt")[0].GetElementsByTagName("h1")[0].TextContent.Replace("Настольная игра ", "");

            var str_price = "";
            try
            {
                str_price = document.GetElementsByClassName("card__price__wrapper")[0].TextContent.Split('р')[0].Trim().Replace(" ", "");
            }
            catch (Exception)
            {
                return null;
                throw;
            }
          
            var image = "https://www.igrotime.ru" + document.GetElementsByClassName("col-lf")[0].
                GetElementsByTagName("img")[0].GetAttribute("src");

            string age_str = document.GetElementsByClassName("characts__item")[0].GetElementsByTagName("a")[0].TextContent.Split()[1];

            string players_count = document.GetElementsByClassName("characts__item")[1].GetElementsByTagName("a")[0].TextContent;

                
            string time = document.GetElementsByClassName("characts__item")[2].GetElementsByTagName("span")[0].TextContent.Split('-')[0];

            var description_div = document.GetElementsByClassName("tabs__item tabs__item--active base-content")[0];

            var description_list = description_div.GetElementsByTagName("p").Where(x => x.Children.Length == 0).ToList();

            if (description_list.Count == 0)
            {
                description_list = description_div.GetElementsByTagName("div")[0].Children[0].GetElementsByTagName("div").
                    Where(x => x.TextContent.Length > 1).ToList();
            }

            if (description_list.Count == 0)
            {
                description_list = description_div.GetElementsByTagName("div")[0].GetElementsByTagName("div").
                    Where(x => x.TextContent.Length > 1).ToList();
            }
            var description = "";

            
            foreach (var item in description_list)
            {
                description += item.TextContent;
            }
            
            return new BoardGame(name, description, Int32.Parse(str_price), image, age_str, time, players_count);
        }
    }
}
