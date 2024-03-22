using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.Domain.Core.Abstractions
{
    public interface IDbExecutor
    {
        Task<T[]> QueryAsync<T>(string sql, object parameters);
    }
}
