using System.Data;
using Dapper;

namespace Persistence.TypeHandlers.DateTimeHandlers;

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = TimeZoneInfo.ConvertTimeToUtc(value);
    }

    public override DateTime Parse(object value)
    {
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
        var utcDateTime = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTime(utcDateTime, timeZoneInfo);
    }
}
