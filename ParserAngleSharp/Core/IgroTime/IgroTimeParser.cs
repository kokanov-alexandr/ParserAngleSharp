using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

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
            var name = document.GetElementsByClassName("col-rt")[0].GetElementsByTagName("h1")[0].TextContent
                .Replace("Настольная игра ", "");

            var str_price = "";

            string[] players;
            int players_min;
            int? players_max;

            try
            {
                str_price = document.GetElementsByClassName("card__price__wrapper")[0].TextContent
                    .Split('р')[0].Trim().Replace(" ", "");

                string str_players_count = document.GetElementsByClassName("characts__item")[1].GetElementsByTagName("a")[0].TextContent;
                players = str_players_count.Split('-');
                players_min = Int32.Parse(players[0]);

                if (players.Length != 2)
                {
                    players_max = null;
                }
                else
                {
                    players_max = Int32.Parse(players[1]);
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
          
            var image = "https://www.igrotime.ru" + document.GetElementsByClassName("col-lf")[0].
                GetElementsByTagName("img")[0].GetAttribute("src");

            int age = Int32.Parse(document.GetElementsByClassName("characts__item")[0].GetElementsByTagName("a")[0].TextContent.Split()[1]);


            string[] array_time = document.GetElementsByClassName("characts__item")[2].GetElementsByTagName("span")[0].TextContent.Split('-');
            int time;

            if (array_time.Length != 2)
            {
                try
                {
                    time = Int32.Parse(array_time[0].Split(' ')[1]);
                }   
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            else
            {
                time = Int32.Parse(array_time[0]);
            }

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
            
            return new BoardGame(name, description, Int32.Parse(str_price), image, age, time, players_min, players_max);
        }
    }
}
