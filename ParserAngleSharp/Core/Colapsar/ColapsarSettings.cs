namespace ParserAngleSharp.Core.Colapsar
{
    class ColapsarSettings : IParserSettings
    {
        public ColapsarSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }

        public string BaseUrl { get; set; } = "https://colapsar.ru/catalog/podbor_podarka/";
        public string Prefix { get; set; } = "?PAGEN_1={CurrentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
