#region

using System.Collections.Generic;
using System.Threading.Tasks;
using GiphyCat.Model;

#endregion

namespace GiphyCat.DataService
{
    internal class DesignGiphyDataService : IGiphyDataService
    {
        #region Implementation of IGiphyDataService

        public Task<GiphyFeedResponse> GetTrendingAsync(int limit, int offset)
        {
            return Task.Factory.StartNew(() => new GiphyFeedResponse
            {
                pagination =  new GiphyPagination(){count =  0, total_count =  1},
                data = new List<GiphyItem>
                {
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media.giphy.com/media/8vEhb7zhaTsv6/giphy.mp4"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media0.giphy.com/media/8vEhb7zhaTsv6/giphy_s.gif"
                            }
                        }
                    },
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media.giphy.com/media/TtdorFbPwFyso/giphy.mp4"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media4.giphy.com/media/TtdorFbPwFyso/giphy_s.gif"
                            }
                        }
                    },
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media1.giphy.com/media/Nq4bSnsWP91Ze/giphy.gif"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media1.giphy.com/media/Nq4bSnsWP91Ze/giphy_s.gif"
                            }
                        }
                    }
                }
            });
        }

        public Task<GiphyFeedResponse> SearchAsync(string query, int limit, int offset)
        {
            return Task.Factory.StartNew(() => new GiphyFeedResponse
            {
                pagination = new GiphyPagination() { count = 0, total_count = 1 },
                data = new List<GiphyItem>
                {
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media.giphy.com/media/QydSm2fMx1KDe/giphy.mp4"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media1.giphy.com/media/QydSm2fMx1KDe/giphy_s.gif"
                            }
                        }
                    },
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media.giphy.com/media/TtdorFbPwFyso/giphy.mp4"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media4.giphy.com/media/TtdorFbPwFyso/giphy_s.gif"
                            }
                        }
                    },
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media1.giphy.com/media/Nq4bSnsWP91Ze/giphy.gif"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media1.giphy.com/media/Nq4bSnsWP91Ze/giphy_s.gif"
                            }
                        }
                    }
                }
            });
        }

        public Task<GiphyFeedResponse> GetGifsByIdAsync(IEnumerable<string> ids)
        {
            return Task.Factory.StartNew(() => new GiphyFeedResponse
            {
                pagination = new GiphyPagination() { count = 0, total_count = 1 },
                data = new List<GiphyItem>
                {
                    new GiphyItem
                    {
                        images = new GiphyImages
                        {
                            original = new GiphyOriginalUrl
                            {
                                mp4 = "http://media1.giphy.com/media/Nq4bSnsWP91Ze/giphy.gif"
                            },
                            original_still = new GiphyImageUrl
                            {
                                url = "http://media1.giphy.com/media/Nq4bSnsWP91Ze/giphy_s.gif"
                            }
                        }
                    }
                }
            });
        }

        public Task<SingleGiphyResponse> RandomGifAsync(string tag)
        {
            return Task.Factory.StartNew(() => new SingleGiphyResponse
            {
                data = new GiphyItem
                {
                    images = new GiphyImages
                    {
                        original = new GiphyOriginalUrl
                        {
                            mp4 = "http://media.giphy.com/media/TtdorFbPwFyso/giphy.mp4"
                        },
                        original_still = new GiphyImageUrl
                        {
                            url = "http://media4.giphy.com/media/TtdorFbPwFyso/giphy_s.gif"
                        }
                    }
                }
            });
        }

        public Task<SingleGiphyResponse> GetGifByIdAsync(string id)
        {
            return Task.Factory.StartNew(() => new SingleGiphyResponse
            {
                data = new GiphyItem
                {
                    images = new GiphyImages
                    {
                        original = new GiphyOriginalUrl
                        {
                            mp4 = "http://media.giphy.com/media/TtdorFbPwFyso/giphy.mp4"
                        },
                        original_still = new GiphyImageUrl
                        {
                            url = "http://media4.giphy.com/media/TtdorFbPwFyso/giphy_s.gif"
                        }
                    }
                }
            });
        }

        public Task<SingleGiphyResponse> TranslateToGifAsync(string termOrPhrase)
        {
            return Task.Factory.StartNew(() => new SingleGiphyResponse
            {
                data = new GiphyItem
                {
                    images = new GiphyImages
                    {
                        original = new GiphyOriginalUrl
                        {
                            mp4 = "http://media.giphy.com/media/TtdorFbPwFyso/giphy.mp4"
                        },
                        original_still = new GiphyImageUrl
                        {
                            url = "http://media4.giphy.com/media/TtdorFbPwFyso/giphy_s.gif"
                        }
                    }
                }
            });
        }

        #endregion
    }
}