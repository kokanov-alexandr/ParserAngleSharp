namespace ParserAngleSharp.Core.MrGeek
{
    internal class LeFuturSettings : IParserSettings
    {
        
        public string BaseUrl { get; set; }
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public LeFuturSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            Prefix = "?page={CurrentId}";
            BaseUrl = "https://lefutur.ru/";
        }

    }
}
