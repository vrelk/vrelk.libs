using System.ComponentModel.DataAnnotations.Schema;

namespace Vrelk.Libs.LibreNMS.DB.Types.Results;
public class ClientSearch
{
    [Column("mac_address")]
    public string MacAddress { get; set; }

    [Column("sysname")]
    public string SwitchName { get; set; }

    [Column("hostname")]
    public string SwitchIp { get; set; }

    [Column("ifname")]
    public string Interface {  get; set; }
}
