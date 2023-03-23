using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAngleSharp.Core.Mosigra
{
    internal class HobbygamesSettings : IParserSettings
    {
        public string BaseUrl { get; set; }
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public HobbygamesSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            Prefix = "?page={CurrentId}";
            BaseUrl = "https://hobbygames.ru/nastolnie";
        }
    }
}
