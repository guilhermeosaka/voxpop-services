using System.Data;
using Npgsql;

namespace Voxpop.Core.Infrastructure.Persistence.Dapper;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection() => new NpgsqlConnection(connectionString);
}