#region

using System.Collections.Generic;
using System.Threading.Tasks;
using GiphyCat.Model;

#endregion

namespace GiphyCat.DataService
{
    public interface IGiphyDataService
    {
        /// <summary>
        ///     Fetch GIFs currently trending online. The data returned mirrors that used to create The Hot 100 list of GIFs on
        ///     Giphy.
        /// </summary>
        /// <param name="limit">number of results to return, maximum 100. Default 25.</param>
        /// <param name="offset">results offset, defaults to 0.</param>
        /// <returns>GiphyFeed of the search response.</returns>
        Task<GiphyFeedResponse> GetTrendingAsync(int limit = 25, int offset = 0);

        /// <summary>
        ///     Search all Giphy GIFs for a word or phrase. Punctuation will be stripped and ignored.
        /// </summary>
        /// <param name="query">search query term or phrase</param>
        /// <param name="limit">number of results to return, maximum 100. Default 25.</param>
        /// <param name="offset">results offset, defaults to 0.</param>
        /// <returns>GiphyFeed of the search response.</returns>
        Task<GiphyFeedResponse> SearchAsync(string query, int limit = 25, int offset = 0);

        /// <summary>
        ///     A multiget version of the get GIF by ID endpoint.
        /// </summary>
        /// <param name="ids">An IEnumerable containing the IDs to fetch.</param>
        /// <returns>GiphyFeedResponse of the requested gifs.</returns>
        Task<GiphyFeedResponse> GetGifsByIdAsync(IEnumerable<string> ids);

        /// <summary>
        ///     Returns a random GIF, limited by tag. Excluding the tag parameter will return a random GIF from the Giphy catalog.
        /// </summary>
        /// <param name="tag">Optional: the GIF tag to limit randomness by</param>
        /// <returns>SingleGiphyResponse of a random gif.</returns>
        Task<SingleGiphyResponse> RandomGifAsync(string tag = null);

        /// <summary>
        ///     Returns meta data about a GIF, by GIF id.
        /// </summary>
        /// <param name="id">the id of the gif</param>
        /// <returns>SingleGiphyResponse of the gif requested.</returns>
        Task<SingleGiphyResponse> GetGifByIdAsync(string id);

        /// <summary>
        ///     This is prototype endpoint for using Giphy as a translation engine for a GIF dialect. The translate API draws on
        ///     search, but uses the Giphy "special sauce" to handle translating from one vocabulary to another. In this case,
        ///     words and phrases to GIFs.
        /// </summary>
        /// <param name="termOrPhrase">term or phrase to translate into a GIF</param>
        /// <returns>SingleGiphyResponse with the translated gif.</returns>
        Task<SingleGiphyResponse> TranslateToGifAsync(string termOrPhrase);
    }
}