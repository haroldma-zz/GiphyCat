namespace GiphyCat.Models
{
    public class GiphyItem
    {
        public string type { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string bitly_gif_url { get; set; }
        public string bitly_url { get; set; }
        public string embed_url { get; set; }
        public string username { get; set; }
        public string source { get; set; }
        public string rating { get; set; }
        public string trending_datetime { get; set; }
        public GiphyImages GiphyImages { get; set; }
    }
}