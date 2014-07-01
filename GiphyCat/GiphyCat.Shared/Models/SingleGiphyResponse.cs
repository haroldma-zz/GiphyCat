using System;
using System.Collections.Generic;
using System.Text;

namespace GiphyCat.Models
{
    public class SingleGiphyResponse : GiphyBaseResponse
    {
        public GiphyItem data { get; set; }
    }
}
