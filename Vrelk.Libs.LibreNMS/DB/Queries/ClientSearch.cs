using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vrelk.Libs.LibreNMS.DB.Queries;

public class ClientSearch
{
    private readonly MySqlConnection _connection;

    internal ClientSearch(MySqlConnection connection)
    {
        _connection = connection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<List<Types.Results.ClientSearch>> Mac2SwitchportAsync(string mac)
    {
        var macObj = new Vrelk.Libs.IP.MacAddress(mac);
        var result = await _connection.QueryAsync<Types.Results.ClientSearch>("SELECT DISTINCT fdb.mac_address, d.sysname, d.hostname, p.ifname FROM ports_fdb AS fdb, devices AS d, ports AS p WHERE fdb.port_id NOT IN(SELECT local_port_id FROM links) AND fdb.port_id NOT IN (SELECT remote_port_id FROM links WHERE remote_port_id IS NOT NULL) AND fdb.device_id = d.device_id AND fdb.port_id = p.port_id AND p.iftype = \"ethernetcsmacd\" AND fdb.mac_address = @macaddr ORDER BY fdb.updated_at DESC LIMIT 1;", new { macObj.NoDelimiterLowercase });
    
        return result.ToList() ?? [];
    }
}
