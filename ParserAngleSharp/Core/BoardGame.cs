using System;

namespace ParserAngleSharp.Core
{
    public class BoardGame
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string  Image { get; set; }
        public string Age { get; set; }
        public string PlayTime { get; set; }
        public string PlayersNumber { get; set; }

        public BoardGame(string Name, string Description, int Price, string Image, string Age, string PlayTime, string PlayersNumber) 
        {
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Image = Image;
            this.Age = Age;
            this.PlayTime = PlayTime;
            this.PlayersNumber = PlayersNumber;
        }
        public BoardGame() { }



    }
}
