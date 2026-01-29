using System.Data;

namespace Voxpop.Core.Infrastructure.Persistence.Common.Dapper;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}