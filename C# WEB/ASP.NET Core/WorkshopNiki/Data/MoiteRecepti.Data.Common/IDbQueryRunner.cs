using System;
using System.Threading.Tasks;

namespace MoiteRecepti.Data.Common
{
    public interface IDbQueryRunner : IDisposable
    {
        Task RunQueryAsync(string query, params object[] parameters);
    }
}
