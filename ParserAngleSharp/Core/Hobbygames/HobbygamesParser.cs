using AngleSharp.Html.Dom;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;

namespace ParserAngleSharp.Core.Mosigra
{
    internal class HobbygamesParser : IParser
    {   
        public List<string> GetElementsPagesPath(IHtmlDocument document)
        {
            var items = document.GetElementsByClassName("product-item__content");
            var result = new List<string>();
            foreach (var item in items)
            {
                var element_page = item.GetElementsByTagName("a")[0].GetAttribute("href");
                result.Add(element_page);
            }
            return result;
        }

        public BoardGame ParseElement(IHtmlDocument document)
        {
            var name = document.GetElementsByClassName("product-info__main")[0].GetElementsByTagName("h1")[0].TextContent;
                
            var str_price = document.GetElementsByClassName("price-item")[0].TextContent.Trim().Replace(" ", "");

            int price = Int32.Parse(str_price = str_price.Replace(((char)(160)).ToString(), "").Replace("₽", ""));

            var image = document.GetElementsByClassName("product-info__images")[0].GetElementsByTagName("a")[0].GetAttribute("href");

            int age = Int32.Parse(document.GetElementsByClassName("age")[0].Children[0].TextContent.Replace("+", ""));

            var str_players_count = document.GetElementsByClassName("players")[0].GetElementsByTagName("span")[0].
                TextContent.Replace(" игрока", "").Replace(" игроков", "");

            int players_min = Int32.Parse(str_players_count.Split('-')[0].Split('+')[0]);
            int players_max;
            try
            {
                players_max = Int32.Parse(str_players_count.Split('-')[1]);

            }
            catch (Exception)
            {
                players_max = 0;
            }

            var array_time = document.GetElementsByClassName("time")[0].GetElementsByTagName("span")[0].TextContent.Split('-');
            int time;

            if (array_time.Length != 2)
            {
                time = Int32.Parse(array_time[0].Split(' ')[0].Replace("+", ""));
            }
            else
            {
                time = Int32.Parse(array_time[0]);
            }

            var description_div = document.GetElementsByClassName("desc-text")[0];

            var description_list = description_div.GetElementsByTagName("p");

            var description = "";

            foreach (var item in description_list)
            {
                description += item.TextContent;
            }
            return new BoardGame(name, description, price, image, age, time, players_min, players_max);
        }
    }

   
}
