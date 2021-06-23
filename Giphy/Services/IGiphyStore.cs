using Giphy.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giphy.Services
{
    public interface IGiphyStore
    {
        /// <summary>
        /// this method should be extended in future to get offset for multiple paging results
        /// </summary>
        /// <param name="queary"></param>
        /// <param name="urls"></param>
        /// <returns></returns>
        Task AddGifsSearchResults(string queary, GiphySearchResult urls);

        /// <summary>
        /// we should change this method in future to suppoert paging ability
        /// </summary>
        /// <param name="queary"></param>
        /// <returns>urls results</returns>
        Task<GiphySearchResult> GetGifsSearchResults(string queary);

    }
}
