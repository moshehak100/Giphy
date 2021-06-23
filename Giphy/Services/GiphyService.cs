using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Giphy.DataStructures;
using Giphy.Interfaces;
using Giphy.Managers;
using Giphy.Services;
using GiphyDotNet.Tools;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Giphy.Services
{
    public class GiphyService : IGiphyService
    {
        private readonly IWebManager _webManager = new WebManager();
        private readonly string _authKey;

        private const string BaseUrl = "http://api.giphy.com/";
        private const string BaseGif = "v1/gifs";
        private IGiphyStore _db;

        /// <summary>
        /// Initialize Giphy Manager.
        /// </summary>
        /// <param name="authKey">Key used for authentication. By default set to the public beta key.</param>
        public GiphyService(IGiphyStore db, IConfiguration config)
        {
            _authKey = config.GetValue<string>("GiphyAPI_Key");
            _db = db;
        }

        /// <summary>
        /// Search for GIFs.
        /// </summary>
        /// <param name="searchParameter">Required: Used to query the search engine.</param>
        /// <returns>A GifSearch Result object.</returns>
        public async Task<GiphySearchResult> SearchGif(SearchParameter searchParameter)
        {
            GiphySearchResult searchRes = null;

            if (string.IsNullOrEmpty(searchParameter.Query))
            {
                throw new FormatException("Must set query in order to search.");
            }

            searchRes = await _db.GetGifsSearchResults(searchParameter.Query);

            if (searchRes == null)
            {
                var nvc = new NameValueCollection();
                nvc.Add("api_key", _authKey);
                nvc.Add("q", searchParameter.Query);
                nvc.Add("limit", searchParameter.Limit.ToString());
                nvc.Add("offset", searchParameter.Offset.ToString());

                var result =
                    await _webManager.GetData(new Uri($"{BaseUrl}{BaseGif}/search{UriExtensions.ToQueryString(nvc)}"));
                if (!result.IsSuccess)
                {
                    throw new WebException($"Failed to get GIFs: {result.ResultJson}");
                }

                searchRes = JsonConvert.DeserializeObject<GiphySearchResult>(result.ResultJson);

                _db.AddGifsSearchResults(searchParameter.Query, searchRes); // we don't want to wait here
            }

            return searchRes;
        }

        public async Task<GiphySearchResult> GetTrendingGifs(TrendingParameter trendingParameter)
        {
            var nvc = new NameValueCollection();
            nvc.Add("api_key", _authKey);
            nvc.Add("limit", trendingParameter.Limit.ToString());

            var result =
                await _webManager.GetData(new Uri($"{BaseUrl}{BaseGif}/trending{UriExtensions.ToQueryString(nvc)}"));
            if (!result.IsSuccess)
            {
                throw new WebException($"Failed to get GIF: {result.ResultJson}");
            }

            return JsonConvert.DeserializeObject<GiphySearchResult>(result.ResultJson);
        }

    }
}
