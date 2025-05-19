using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrelk.Libs.LibreNMS.DB.Queries;

public class DeviceSearch
{
    private readonly MySqlConnection _connection;

    //private const string QUERYBASE = @"SELECT `devices`.`device_id` AS `device_id`, `devices`.`inserted` AS `inserted`, `devices`.`hostname` AS `hostname`, `devices`.`sysName` AS `sysName`, `devices`.`display` AS `display`, INET6_NTOA(`devices`.`ip`) AS `ip`, `devices`.`overwrite_ip` AS `overwrite_ip`, `devices`.`community` AS `community`, `devices`.`authlevel` AS `authlevel`, `devices`.`authname` AS `authname`, `devices`.`authpass` AS `authpass`, `devices`.`authalgo` AS `authalgo`, `devices`.`cryptopass` AS `cryptopass`, `devices`.`cryptoalgo` AS `cryptoalgo`, `devices`.`snmpver` AS `snmpver`, `devices`.`port` AS `port`, `devices`.`transport` AS `transport`, `devices`.`timeout` AS `timeout`, `devices`.`retries` AS `retries`, `devices`.`snmp_disable` AS `snmp_disable`, `devices`.`bgpLocalAs` AS `bgpLocalAs`, `devices`.`sysObjectID` AS `sysObjectID`, `devices`.`sysDescr` AS `sysDescr`, `devices`.`sysContact` AS `sysContact`, `devices`.`version` AS `version`, `devices`.`hardware` AS `hardware`, `devices`.`features` AS `features`, `devices`.`location_id` AS `location_id`, `devices`.`os` AS `os`, `devices`.`status` AS `status`, `devices`.`status_reason` AS `status_reason`, `devices`.`ignore` AS `ignore`, `devices`.`disabled` AS `disabled`, `devices`.`uptime` AS `uptime`, `devices`.`agent_uptime` AS `agent_uptime`, `devices`.`last_polled` AS `last_polled`, `devices`.`last_poll_attempted` AS `last_poll_attempted`, `devices`.`last_polled_timetaken` AS `last_polled_timetaken`, `devices`.`last_discovered_timetaken` AS `last_discovered_timetaken`, `devices`.`last_discovered` AS `last_discovered`, `devices`.`last_ping` AS `last_ping`, `devices`.`last_ping_timetaken` AS `last_ping_timetaken`, `devices`.`purpose` AS `purpose`, `devices`.`type` AS `type`, `devices`.`serial` AS `serial`, `devices`.`icon` AS `icon`, `devices`.`poller_group` AS `poller_group`, `devices`.`override_sysLocation` AS `override_sysLocation`, `devices`.`notes` AS `notes`, `devices`.`port_association_mode` AS `port_association_mode`, `devices`.`max_depth` AS `max_depth`, `devices`.`disable_notify` AS `disable_notify`, `devices`.`ignore_status` AS `ignore_status` ";

    internal DeviceSearch(MySqlConnection connection)
    {
        _connection = connection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<List<Types.DbObjects.Device>> SearchByNameOrIPAsync(string term)
    {
        var result = await _connection.QueryAsync<Types.DbObjects.Device>(QueryLoader.LoadAssemblyResource("Queries.SQL.SearchByNameOrIP.mysql")); //(QUERYBASE + @"FROM `devices` WHERE(`devices`.`hostname` = @SearchTerm) OR (`devices`.`sysName` = @SearchTerm) OR (`devices`.`overwrite_ip` = @SearchTerm)", new { term });

        return result.ToList() ?? [];
    }

    public async Task<List<dynamic>> GetOfflineDevicesAsync()
    {
        var result = await _connection.QueryAsync(QueryLoader.LoadAssemblyResource("Queries.SQL.GetOfflineDevices.mysql")); //(QUERYBASE + @", `eventlog`.`datetime` AS `event_timestamp`, `eventlog`.`message` AS `event_msg` FROM `devices` LEFT JOIN `eventlog` ON `devices`.`device_id` = `eventlog`.`device_id` WHERE(`devices`.`status` = FALSE) AND (`eventlog`.`type` = 'down') AND (`eventlog`.`event_id` = (SELECT MAX(`eventlog`.`event_id`) FROM `eventlog` WHERE `eventlog`.`device_id` = `devices`.`device_id` AND `eventlog`.`type` = 'down')) GROUP BY `devices`.`device_id`");

        return result.ToList() ?? [];
    }
}
