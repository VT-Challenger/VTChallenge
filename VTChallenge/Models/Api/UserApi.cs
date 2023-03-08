namespace VTChallenge.Models.Api
{
    public class UserApi
    {
        public DataApi Data { get; set; }
    }
    public class Card
    {
        public string Small { get; set; }
        public string Large { get; set; }
        public string Wide { get; set; }
        public string Id { get; set; }
    }

    public class DataApi
    {
        public string Puuid { get; set; }
        public string Region { get; set; }
        public int AccountLevel { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public Card Card { get; set; }
        public string LastUpdate { get; set; }
        public long LastUpdateRaw { get; set; }
    }
}
