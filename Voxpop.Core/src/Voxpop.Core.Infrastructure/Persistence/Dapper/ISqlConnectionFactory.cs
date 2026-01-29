using System.Data;

namespace Voxpop.Core.Infrastructure.Persistence.Dapper;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}