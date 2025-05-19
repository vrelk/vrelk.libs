using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrelk.Libs.LibreNMS.DB.Queries;

public class EventLogs
{
    private readonly MySqlConnection _connection;

    private const string QUERYBASE = @"SELECT `eventlog`.`event_id` AS `event_id`, `eventlog`.`device_id` AS `device_id`, `eventlog`.`datetime` AS `datetime`, `eventlog`.`message` AS `message`, `eventlog`.`type` AS `type`, `eventlog`.`reference` AS `reference`, `eventlog`.`username` AS `username`, `eventlog`.`severity` AS `severity` ";

    internal EventLogs(MySqlConnection connection)
    {
        _connection = connection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<List<DB.Types.DbObjects.EventLog>> GetDeviceEventsAsync(int deviceId, string? eventType = null, int limit = 100)
    {
        if (deviceId <= 0) throw new ArgumentOutOfRangeException("deviceId");
        if (limit <= 0) throw new ArgumentOutOfRangeException("limit");

        IEnumerable<DB.Types.DbObjects.EventLog>? result;
        if (!string.IsNullOrWhiteSpace(eventType))
        {
            result = await _connection.QueryAsync<Types.DbObjects.EventLog>(QUERYBASE + @"FROM `eventlog` WHERE(`eventlog`.`device_id` = @DeviceId) AND (`eventlog`.`type` = @EventType) ORDER BY `eventlog`.`event_id` DESC LIMIT @Limit",
                new
                {
                    DeviceId = deviceId,
                    EventType = eventType,
                    Limit = limit
                });
        }
        else
        {
            result = await _connection.QueryAsync<Types.DbObjects.EventLog>(QUERYBASE + @"FROM `eventlog` WHERE(`eventlog`.`device_id` = @DeviceId) ORDER BY `eventlog`.`event_id` DESC LIMIT @Limit",
                new
                {
                    DeviceId = deviceId,
                    Limit = limit
                });
        }

        return result.ToList() ?? [];
    }

    public async Task<List<DB.Types.DbObjects.EventLog>> GetUpDownEventsAsync(int deviceId, int limit = 100)
    {
        if (deviceId <= 0) throw new ArgumentOutOfRangeException("deviceId");
        if (limit <= 0) throw new ArgumentOutOfRangeException("limit");

        IEnumerable<DB.Types.DbObjects.EventLog>? result;
        result = await _connection.QueryAsync<Types.DbObjects.EventLog>(QUERYBASE + @"FROM `eventlog` WHERE(`eventlog`.`device_id` = @DeviceId) AND ( (`eventlog`.`type` = 'up') OR (`eventlog`.`type` = 'down')) ORDER BY `eventlog`.`event_id` DESC LIMIT @Limit",
            new
            {
                DeviceId = deviceId,
                Limit = limit
            });


        return result.ToList() ?? [];
    }
}
