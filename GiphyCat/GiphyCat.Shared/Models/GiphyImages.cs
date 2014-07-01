namespace GiphyCat.Models
{
    public class GiphyImages
    {
        public GiphyMp4Url fixed_height { get; set; }
        public GiphyImageUrl fixed_height_still { get; set; }
        public GiphyImageUrl fixed_height_downsampled { get; set; }
        public GiphyMp4Url fixed_width { get; set; }
        public GiphyImageUrl fixed_width_still { get; set; }
        public GiphyMp4Url fixed_width_downsampled { get; set; }
        public GiphyOriginalUrl original { get; set; }
        public GiphyImageUrl original_still { get; set; }
    }
}