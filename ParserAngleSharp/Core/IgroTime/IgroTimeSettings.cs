using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserAngleSharp.Core.IgroTime
{
    internal class IgroTimeSettings : IParserSettings
    {
        public string BaseUrl { get; set; }
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public IgroTimeSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            Prefix = "?page={CurrentId}";
            BaseUrl = "https://www.igrotime.ru/allgames/";
        }
    }
}
