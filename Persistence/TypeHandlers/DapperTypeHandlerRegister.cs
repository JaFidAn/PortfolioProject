using Dapper;
using Persistence.TypeHandlers.DateTimeHandlers;
namespace Persistence.TypeHandlers;

public static class DapperTypeHandlerRegister
{
    public static void RegisterHandlers()
    {
        SqlMapper.AddTypeHandler(new DateTimeHandler());
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
    }
}
