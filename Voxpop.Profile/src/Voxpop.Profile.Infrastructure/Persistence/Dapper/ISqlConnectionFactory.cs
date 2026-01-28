using System.Data;

namespace Voxpop.Profile.Infrastructure.Persistence.Dapper;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}