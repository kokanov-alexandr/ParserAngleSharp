using System;

namespace ParserAngleSharp.Core
{
    public class BoardGame
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public int PlayTime { get; set; }
        public int PlayersNumberMin { get; set; }
        public int? PlayersNumberMax{ get; set; }

        public BoardGame(string Name, string Description, int Price, string Image, int Age, int PlayTime, int PlayersNumberMin, int? PlayersNumberMax) 
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Image = Image;
            this.Age = Age;
            this.PlayTime = PlayTime;
            this.PlayersNumberMin = PlayersNumberMin;
            this.PlayersNumberMax = PlayersNumberMax;
        }
    }
}
