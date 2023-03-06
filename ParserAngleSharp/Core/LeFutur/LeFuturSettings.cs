namespace ParserAngleSharp.Core.MrGeek
{
    internal class LeFuturSettings : IParserSettings
    {
        public LeFuturSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }

        public string BaseUrl { get; set; } = "https://lefutur.ru/";
        public string Prefix { get; set; } = "?page={CurrentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }

    }
}
