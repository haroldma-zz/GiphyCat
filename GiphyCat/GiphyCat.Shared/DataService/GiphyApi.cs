#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GiphyCat.AppException;
using GiphyCat.Model;
using Newtonsoft.Json;

#endregion

namespace GiphyCat.DataService
{
    public class GiphyApi
    {
        #region Private Properties and Methods

        private const string ApiKey =
#if DEBUG
            //Beta key
            "dc6zaTOxFJmzC";
#else
    //Production key. Instructions to get one at https://github.com/Giphy/GiphyAPI
        "???";
#endif

        private const string Host = "https://api.giphy.com";
        private const string BasePath = Host + "/v1/gifs/";
        private const string TrendingPath = BasePath + "trending";
        private const string SearchPath = BasePath + "search";
        private const string TranslatePath = BasePath + "translate";
        private const string RandomPath = BasePath + "random";

        /// <summary>
        ///     Generates url encoded string with the api key.
        ///     If dictionary is pass, those parameters are appended also.
        /// </summary>
        /// <param name="paramsDictionary">Optional parameters</param>
        /// <returns></returns>
        private string GetUrlEncodedParameters(
            IReadOnlyCollection<KeyValuePair<string, string>> paramsDictionary = null)
        {
            var sb = new StringBuilder();

            sb.Append("?api_key=" + ApiKey);

            if (paramsDictionary == null) return sb.ToString();

            for (var i = 0; i < paramsDictionary.Count; i++)
            {
                sb.Append("&");
                sb.Append(paramsDictionary.ElementAt(i).Key);
                sb.Append("=");
                sb.Append(paramsDictionary.ElementAt(i).Value);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Generic method that calls the given url and deserializes the response as a GiphyFeedResponse.
        ///     Optional parameters are passed if present. Throws exception if required parameters are not present
        /// </summary>
        /// <param name="url">The endpoint to call.</param>
        /// <param name="limit">number of results to return, maximum 100. Default 25.</param>
        /// <param name="offset">results offset, defaults to 0.</param>
        /// <param name="query">If calling the search endpoint you must pass a query.</param>
        /// <param name="ids">If calling the get gifs by id you must fast a comma seperated list of ids.</param>
        /// <returns></returns>
        private async Task<GiphyFeedResponse> GetGiphyFeedAsync(string url, int limit = 25, int offset = 0,
                                                                string query = null, string ids = null)
        {
            var paramDictionary = new Dictionary<string, string>();

            if (url == SearchPath)
                if (string.IsNullOrEmpty(query)) throw new ArgumentNullException("query");
                else paramDictionary.Add("query", query);

            if (url != BasePath)
            {
                paramDictionary.Add("limit", limit.ToString());
                paramDictionary.Add("offset", offset.ToString());
            }
            else if (string.IsNullOrEmpty(ids)) throw new ArgumentNullException("ids");
            else paramDictionary.Add("ids", ids);

            url += GetUrlEncodedParameters(paramDictionary);

            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync(url);
                var json = await resp.Content.ReadAsStringAsync();
                var parseResp = await DeserializeAsync<GiphyFeedResponse>(json);

                //Check for errors in response
                ThrowIfError(resp, parseResp);

                return parseResp;
            }
        }

        /// <summary>
        ///     Generic method that calls the given url and deserializes the response as a SingleGiphyResponse.
        ///     Optional parameters are passed if present. Throws exception if required parameters are not present
        /// </summary>
        /// <param name="url">The endpoint to call.</param>
        /// <param name="id">If calling get gif by id endpoint you must pass an id.</param>
        /// <param name="tag">If calling random endpoint you must pass a tag.</param>
        /// <param name="termOrPhrase">If calling the translate endpoint you must pass a term or phrase.</param>
        /// <returns></returns>
        private async Task<SingleGiphyResponse> GetSingleGiphyAsync(string url, string id = null, string tag = null,
                                                                    string termOrPhrase = null)
        {
            var paramDictionary = new Dictionary<string, string>();

            if (url == BasePath)
                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
                else url += id;
            if (url == TranslatePath)
                if (string.IsNullOrEmpty(termOrPhrase)) throw new ArgumentNullException("termOrPhrase");
                else paramDictionary.Add("s", termOrPhrase);
            if (url == RandomPath && !string.IsNullOrEmpty(tag)) paramDictionary.Add("tag", tag);

            url += GetUrlEncodedParameters(paramDictionary);

            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync(url);
                var json = await resp.Content.ReadAsStringAsync();
                var parseResp = await DeserializeAsync<SingleGiphyResponse>(json);

                //Check for errors in response
                ThrowIfError(resp, parseResp);

                return parseResp;
            }
        }

        /// <summary>
        ///     Throws an exception if either the response or parse response has an error.
        /// </summary>
        /// <param name="resp">The http response to validate.</param>
        /// <param name="parseResp">The parse response from the content of the http response.</param>
        private void ThrowIfError(HttpResponseMessage resp, GiphyBaseResponse parseResp)
        {
            if (parseResp != null && parseResp.GiphyMeta != null && parseResp.GiphyMeta.status >= 400) throw new ApiException(parseResp.GiphyMeta.msg);
            if (!resp.IsSuccessStatusCode) throw new NetworkException("Problem connecting to server.");
        }

        private async Task<T> DeserializeAsync<T>(string json)
        {
            try
            {
                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json));
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        /// <summary>
        ///     Fetch GIFs currently trending online. The data returned mirrors that used to create The Hot 100 list of GIFs on
        ///     Giphy.
        /// </summary>
        /// <param name="limit">number of results to return, maximum 100. Default 25.</param>
        /// <param name="offset">results offset, defaults to 0.</param>
        /// <returns>GiphyFeed of the search response.</returns>
        public async Task<GiphyFeedResponse> GetTrendingAsync(int limit = 25, int offset = 0)
        {
            return await GetGiphyFeedAsync(TrendingPath, limit, offset);
        }

        /// <summary>
        ///     Search all Giphy GIFs for a word or phrase. Punctuation will be stripped and ignored.
        /// </summary>
        /// <param name="query">search query term or phrase</param>
        /// <param name="limit">number of results to return, maximum 100. Default 25.</param>
        /// <param name="offset">results offset, defaults to 0.</param>
        /// <returns>GiphyFeed of the search response.</returns>
        public async Task<GiphyFeedResponse> SearchAsync(string query, int limit = 25, int offset = 0)
        {
            return await GetGiphyFeedAsync(SearchPath, limit, offset, query);
        }

        /// <summary>
        ///     A multiget version of the get GIF by ID endpoint.
        /// </summary>
        /// <param name="ids">An IEnumerable containing the IDs to fetch.</param>
        /// <returns></returns>
        public async Task<GiphyFeedResponse> GetGifsByIdAsync(IEnumerable<string> ids)
        {
            var sb = new StringBuilder();
            var arr = ids.ToArray();
            for (var i = 0; i < arr.Length; i++)
            {
                if (i != 0) sb.Append(",");
                sb.Append(arr[i]);
            }

            return await GetGiphyFeedAsync(SearchPath, ids: sb.ToString());
        }

        /// <summary>
        ///     Returns a random GIF, limited by tag. Excluding the tag parameter will return a random GIF from the Giphy catalog.
        /// </summary>
        /// <param name="tag">Optional: the GIF tag to limit randomness by</param>
        /// <returns>SingleGiphyResponse of a random gif.</returns>
        public async Task<SingleGiphyResponse> RandomGifAsync(string tag = null)
        {
            return await GetSingleGiphyAsync(RandomPath, tag: tag);
        }

        /// <summary>
        ///     Returns meta data about a GIF, by GIF id.
        /// </summary>
        /// <param name="id">the id of the gif</param>
        /// <returns>SingleGiphyResponse of the gif requested.</returns>
        public async Task<SingleGiphyResponse> GetGifByIdAsync(string id)
        {
            return await GetSingleGiphyAsync(BasePath, id);
        }

        /// <summary>
        ///     This is prototype endpoint for using Giphy as a translation engine for a GIF dialect. The translate API draws on
        ///     search, but uses the Giphy "special sauce" to handle translating from one vocabulary to another. In this case,
        ///     words and phrases to GIFs.
        /// </summary>
        /// <param name="termOrPhrase">term or phrase to translate into a GIF</param>
        /// <returns>SingleGiphyResponse with the translated gif.</returns>
        public async Task<SingleGiphyResponse> TranslateToGifAsync(string termOrPhrase)
        {
            return await GetSingleGiphyAsync(TranslatePath, termOrPhrase: termOrPhrase);
        }
    }
}