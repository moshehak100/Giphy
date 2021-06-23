using Giphy.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giphy.Services
{
    public class InMemoryGiphyStore : IGiphyStore
    {
        private Dictionary<string, GiphySearchResult> _searchResults = new Dictionary<string, GiphySearchResult>();
        private System.Threading.ReaderWriterLockSlim _guard = new System.Threading.ReaderWriterLockSlim();
        public Task AddGifsSearchResults(string queary, GiphySearchResult quearyRes)
        {
            _guard.EnterWriteLock();
            try
            {
                _searchResults.Add(queary, quearyRes);
            }
            finally
            {
                _guard.ExitWriteLock();
            }
            
            return Task.CompletedTask;
        }

        public Task<GiphySearchResult> GetGifsSearchResults(string queary)
        {
            return Task.Run(() =>
            {
                GiphySearchResult res = null;

                _guard.EnterReadLock();
                try
                {
                    if (!_searchResults.TryGetValue(queary, out res))
                    {
                        res = null;
                    }
                }
                finally
                {
                    _guard.ExitReadLock();
                }

                return res;
            });
        }
    }
}
