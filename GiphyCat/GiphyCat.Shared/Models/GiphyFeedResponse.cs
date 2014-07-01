using System.Collections.Generic;

namespace GiphyCat.Models
{
    public class GiphyFeedResponse : GiphyBaseResponse
    {
        public List<GiphyItem> data { get; set; }
        public GiphyPagination GiphyPagination { get; set; }
    }
}