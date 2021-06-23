using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giphy.DataStructures
{
    public class SearchParameter
    {
        /// <summary>
        /// GifSearch query term or phrase
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// (optional) number of results to return, maximum 100. Default 25.
        /// </summary>
        public int Limit { get; set; } = 25;

        /// <summary>
        /// (optional) results offset, defaults to 0.
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}
