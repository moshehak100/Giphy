using Giphy.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giphy.Services
{
    public interface IGiphyService
    {
        Task<GiphySearchResult> SearchGif(SearchParameter searchParameter);
        Task<GiphySearchResult> GetTrendingGifs(TrendingParameter trendingParameter);
    }
}
