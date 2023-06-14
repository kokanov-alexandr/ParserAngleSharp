using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserAngleSharp.Core.Mosigra
{
    internal class LavkaigrParser : IParser
    {
        public List<string> GetElementsPagesPath(IHtmlDocument document)
        {
            var items = document.GetElementsByClassName("photo-block");
            var result = new List<string>();
            foreach (var item in items)
            {
                var element_page = "https://www.lavkaigr.ru/" + item.GetElementsByTagName("a")[0].GetAttribute("href");
                result.Add(element_page);
            }
            return result;
        }

        public BoardGame ParseElement(IHtmlDocument document)   
        {
            var name = document.GetElementsByClassName("game-name")[0].TextContent;


            int price = Int32.Parse(document.GetElementsByClassName("price")[0].TextContent
                .Replace(" " , "").Replace("\n", "").Replace("руб.", ""));

            string[] players;
            int players_min;
            int? players_max;


            var a = document.GetElementsByClassName("info-01")[0]
                .Children[0].Children[1].Children[0];

            string str_players_count = document.GetElementsByClassName("info-01")[0]
                .Children[0].Children[1].Children[0].TextContent
                .Replace(" ", "").Replace("\n", "");

            players = str_players_count.Split('-');
            players_min = Int32.Parse(players[0]);

            if (players.Count() == 2)
            {
                players_max = Int32.Parse(players[1]);
            }
            else
            {
                players_max = null;
            }

            var image = document.GetElementsByClassName("large-img")[0].GetAttribute("src");

            int age = Int32.Parse(document.GetElementsByClassName("info-01")[0]
                .Children[2].Children[1].TextContent.Split()[1]);

            int time = Int32.Parse(document.GetElementsByClassName("info-01")[0]
                .Children[1].Children[1].TextContent
                .Replace(" мин.", "").Replace("\n", "").Replace(" ", "").Split('-')[0]);
    
            var description_div = document.GetElementsByClassName("txt-box box open")[0];

            var description_list = description_div.GetElementsByTagName("p").ToList();

            description_list = description_div.GetElementsByTagName("p")
                .Where(x => x.TextContent.Length > 1).ToList();

            var description = "";


            foreach (var item in description_list)
            {
                description += item.TextContent;
            }

            return new BoardGame(name, description, price, image, age, time, players_min, players_max);
        }
    }
}
