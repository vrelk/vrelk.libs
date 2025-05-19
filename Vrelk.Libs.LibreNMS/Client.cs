using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Vrelk.Libs.LibreNMS.DB;

namespace Vrelk.Libs.LibreNMS;
public class Client
{
    private readonly MySqlConnection _connection;

    public DB.Queries.ClientSearch ClientSearch { get; private set; }
    public DB.Queries.DeviceSearch DeviceSearch { get; private set; }
    public DB.Queries.EventLogs EventLogs { get; private set; }


    public Client(MySqlConnection connection)
    {
        _connection = connection;
        Setup();
    }

    public Client(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
        Setup();

    }

    private void Setup()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        ClientSearch = new(_connection);
        DeviceSearch = new(_connection);
        EventLogs = new(_connection);
    }
}
