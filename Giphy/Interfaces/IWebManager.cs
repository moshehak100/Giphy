using Giphy.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Giphy.Interfaces
{
    internal interface IWebManager
    {
        Task<Result> GetData(Uri uri);
    }
}
