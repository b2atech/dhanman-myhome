using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.Domain.Core.Abstractions;

public interface ISqlConnectionFactory
{
    Task<IDbConnection> CreateSqlConnectionAsync(CancellationToken cancellationToken = default);
}