using System.Data;
using Npgsql;

namespace Voxpop.Profile.Infrastructure.Persistence.Dapper;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection() => new NpgsqlConnection(connectionString);
}