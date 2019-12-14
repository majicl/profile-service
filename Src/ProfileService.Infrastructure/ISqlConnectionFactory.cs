using System.Data;

namespace ProfileService.Infrastructure
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
