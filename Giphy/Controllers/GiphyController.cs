using Giphy.DataStructures;
using Giphy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giphy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiphyController : ControllerBase
    {
        private readonly IGiphyService _giphy;

        public GiphyController(IGiphyService giphy)
        {
            _giphy = giphy;
        }

        [HttpGet("GetTrendingGifs")]
        public async Task<List<string>> GetTrendingGifs()
        {
            // for this demo no Paging implemented, in future we do need to support getting next page results
            GiphySearchResult res = await _giphy.GetTrendingGifs(new DataStructures.TrendingParameter { Limit = 20 });
            List<string> urls = res.Data.Select(item => item.images.original.url).ToList();

            return urls;
        }

        [HttpGet("Search")]
        public async Task<List<string>> Search(string term)
        {
            // for this demo no Paging implemented, in future we do need to support getting next page results
            GiphySearchResult res = await _giphy.SearchGif(new SearchParameter { Query = term, Limit = 20 });
            List<string> urls = res.Data.Select(item => item.images.original.url).ToList();

            return urls;
        }
    }
}
