namespace ParserAngleSharp.Core.Colapsar
{
    class ColapsarSettings : IParserSettings
    {
       
        public string BaseUrl { get; set; }
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public ColapsarSettings(int StartPoint, int EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            Prefix = "?PAGEN_1={CurrentId}";
            BaseUrl = "https://colapsar.ru/catalog/podbor_podarka/";
        }
    }
}
