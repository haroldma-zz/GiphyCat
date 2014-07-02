using System.Collections.Generic;

namespace GiphyCat.Model
{
    public class GiphyFeedResponse : GiphyBaseResponse
    {
        public List<GiphyItem> data { get; set; }
        public GiphyPagination pagination { get; set; }
    }
}