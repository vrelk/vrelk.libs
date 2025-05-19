using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Vrelk.Libs.LibreNMS.DB.DapperExtensions;

namespace Vrelk.Libs.LibreNMS.DB.Types.DbObjects;

/// <summary>
/// Represents a device in the LibreNMS monitoring system, corresponding to the `devices` table in the database.
/// </summary>
[Table("devices")]
public class Device
{
    public Device() { }

    /// <summary>
    /// Gets or sets the unique identifier for the device.
    /// </summary>
    [Column("device_id"), Key]
    public int DeviceId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the device was inserted into the database.
    /// </summary>
    [Column("inserted")]
    public DateTime? Inserted { get; set; }

    /// <summary>
    /// Gets or sets the hostname of the device.
    /// </summary>
    [Column("hostname")]
    public string Hostname { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the system name of the device, as reported by SNMP.
    /// </summary>
    [Column("sysName")]
    public string? SysName { get; set; }

    /// <summary>
    /// Gets or sets the display name for the device.
    /// </summary>
    [Column("display")]
    public string? Display { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the device as a formatted string.
    /// </summary>
    [Column("ip")]
    public string? Ip { get; set; }

    /// <summary>
    /// Gets or sets the overridden IP address for the device, if specified.
    /// </summary>
    [Column("overwrite_ip")]
    public string? OverwriteIp { get; set; }

    /// <summary>
    /// Gets or sets the SNMP community string for the device.
    /// </summary>
    [Column("community")]
    public string? Community { get; set; }

    /// <summary>
    /// Gets or sets the SNMP authentication level (e.g., NoAuthNoPriv, AuthNoPriv, AuthPriv).
    /// </summary>
    [Column("authlevel")]
    public DapperableEnum<AuthLevelEnum>? AuthLevel { get; set; }

    /// <summary>
    /// Gets or sets the SNMP authentication username.
    /// </summary>
    [Column("authname")]
    public string? AuthName { get; set; }

    /// <summary>
    /// Gets or sets the SNMP authentication password.
    /// </summary>
    [Column("authpass")]
    public string? AuthPass { get; set; }


    /// <summary>
    /// Gets or sets the SNMP authentication algorithm (e.g., MD5, SHA).
    /// </summary>
    [Column("authalgo")]
    public DapperableEnum<AuthAlgoEnum>? AuthAlgo { get; set; }

    /// <summary>
    /// Gets or sets the SNMP encryption password.
    /// </summary>
    [Column("cryptopass")]
    public string? CryptoPass { get; set; }

    /// <summary>
    /// Gets or sets the SNMP encryption algorithm (e.g., AES, DES).
    /// </summary>
    [Column("cryptoalgo")]
    public DapperableEnum<CryptoAlgoEnum>? CryptoAlgo { get; set; }

    /// <summary>
    /// Gets or sets the SNMP version used by the device (e.g., v1, v2c, v3).
    /// </summary>
    [Column("snmpver")]
    public DapperableEnum<SnmpVerEnum> SnmpVer { get; set; } = "v2c";

    /// <summary>
    /// Gets or sets the port number used for SNMP communication.
    /// </summary>
    [Column("port")]
    public ushort Port { get; set; } = 161;

    /// <summary>
    /// Gets or sets the transport protocol for SNMP (e.g., udp, tcp).
    /// </summary>
    [Column("transport")]
    public string Transport { get; set; } = "udp";

    /// <summary>
    /// Gets or sets the timeout value for SNMP requests, in seconds.
    /// </summary>
    [Column("timeout")]
    public int? Timeout { get; set; }

    /// <summary>
    /// Gets or sets the number of retries for SNMP requests.
    /// </summary>
    [Column("retries")]
    public int? Retries { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether SNMP is disabled for the device.
    /// </summary>
    [Column("snmp_disable")]
    public bool SnmpDisable { get; set; }

    /// <summary>
    /// Gets or sets the local BGP autonomous system number for the device.
    /// </summary>
    [Column("bgpLocalAs")]
    public uint? BgpLocalAs { get; set; }

    /// <summary>
    /// Gets or sets the SNMP system object ID of the device.
    /// </summary>
    [Column("sysObjectID")]
    public string? SysObjectId { get; set; }

    /// <summary>
    /// Gets or sets the system description of the device, as reported by SNMP.
    /// </summary>
    [Column("sysDescr")]
    public string? SysDescr { get; set; }

    /// <summary>
    /// Gets or sets the system contact information for the device, as reported by SNMP.
    /// </summary>
    [Column("sysContact")]
    public string? SysContact { get; set; }

    /// <summary>
    /// Gets or sets the operating system version of the device.
    /// </summary>
    [Column("version")]
    public string? Version { get; set; }

    /// <summary>
    /// Gets or sets the hardware model or type of the device.
    /// </summary>
    [Column("hardware")]
    public string? Hardware { get; set; }

    /// <summary>
    /// Gets or sets the features or capabilities of the device.
    /// </summary>
    [Column("features")]
    public string? Features { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the location associated with the device.
    /// </summary>
    [Column("location_id")]
    public uint? LocationId { get; set; }

    /// <summary>
    /// Gets or sets the operating system type of the device (e.g., ios, linux).
    /// </summary>
    [Column("os")]
    public string? Os { get; set; }

    /// <summary>
    /// Gets or sets the status of the device (false = down, true = up).
    /// </summary>
    [Column("status")]
    public DeviceStatusEnum Status { get; set; }

    /// <summary>
    /// Gets or sets the reason for the device's current status.
    /// </summary>
    [Column("status_reason")]
    public DapperableEnum<StatusReasonEnum> StatusReason = string.Empty;

    /// <summary>
    /// Gets or sets the reason for the device's current status.
    /// </summary>
    [Column("ignore")]
    public bool Ignore { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device is disabled.
    /// </summary>
    [Column("disabled")]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the uptime of the device, in seconds.
    /// </summary>
    [Column("uptime")]
    public long? Uptime { get; set; }

    /// <summary>
    /// Gets or sets the uptime of the agent running on the device, in seconds.
    /// </summary>
    [Column("agent_uptime")]
    public uint AgentUptime { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last successful poll of the device.
    /// </summary>
    [Column("last_polled")]
    public DateTime? LastPolled { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last poll attempt for the device.
    /// </summary>
    [Column("last_poll_attempted")]
    public DateTime? LastPollAttempted { get; set; }

    /// <summary>
    /// Gets or sets the time taken for the last poll, in seconds.
    /// </summary>
    [Column("last_polled_timetaken")]
    public double? LastPolledTimeTaken { get; set; }

    /// <summary>
    /// Gets or sets the time taken for the last discovery, in seconds.
    /// </summary>
    [Column("last_discovered_timetaken")]
    public double? LastDiscoveredTimeTaken { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last discovery of the device.
    /// </summary>
    [Column("last_discovered")]
    public DateTime? LastDiscovered { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last ping to the device.
    /// </summary>
    [Column("last_ping")]
    public DateTime? LastPing { get; set; }

    /// <summary>
    /// Gets or sets the time taken for the last ping, in seconds.
    /// </summary>
    [Column("last_ping_timetaken")]
    public double? LastPingTimeTaken { get; set; }

    /// <summary>
    /// Gets or sets the purpose or role of the device (e.g., router, switch).
    /// </summary>
    [Column("purpose")]
    public string? Purpose { get; set; }

    /// <summary>
    /// Gets or sets the type of the device (e.g., network, server).
    /// </summary>
    [Column("type")]
    public DapperableEnum<DeviceTypeEnum> Type { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the device.
    /// </summary>
    [Column("serial")]
    public string? Serial { get; set; }

    /// <summary>
    /// Gets or sets the icon associated with the device for display purposes.
    /// </summary>
    [Column("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the poller group identifier for the device.
    /// </summary>
    [Column("poller_group")]
    public int PollerGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to override the system location.
    /// </summary>
    [Column("override_sysLocation")]
    public bool OverrideSysLocation { get; set; }

    /// <summary>
    /// Gets or sets additional notes or comments about the device.
    /// </summary>
    [Column("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the mode for associating ports with the device.
    /// </summary>
    [Column("port_association_mode")]
    public PortAssociationModeEnum PortAssociationMode { get; set; } = PortAssociationModeEnum.IfIndex;

    /// <summary>
    /// Gets or sets the maximum discovery depth for the device.
    /// </summary>
    [Column("max_depth")]
    public int MaxDepth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether notifications are disabled for the device.
    /// </summary>
    [Column("disable_notify")]
    public bool DisableNotify { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to ignore the device's status.
    /// </summary>
    [Column("ignore_status")]
    public bool IgnoreStatus { get; set; }


    public enum DeviceStatusEnum
    {
        DOWN = 0,
        UP = 1
    }

    public enum PortAssociationModeEnum
    {
        IfIndex = 1,
        IfName = 2,
        IfDescr = 3,
        IfAlias = 4
    }

    public enum AuthLevelEnum
    {
        Undefined = 0,
        [DatabaseValue("NoAuthNoPriv")]
        NoAuthNoPriv,
        [DatabaseValue("AuthNoPriv")]
        AuthNoPriv,
        [DatabaseValue("AuthPriv")]
        AuthPriv
    }

    /// <summary>
    /// Authentication algorithm for SNMPv3.
    /// </summary>
    public enum AuthAlgoEnum
    {
        /// <summary>MD5 authentication algorithm.</summary>
        [DatabaseValue("md5")]
        MD5,
        /// <summary>SHA authentication algorithm.</summary>
        [DatabaseValue("sha")]
        SHA,
        /// <summary>SHA-224 authentication algorithm.</summary>
        [DatabaseValue("sha-224")]
        SHA224,
        /// <summary>SHA-256 authentication algorithm.</summary>
        [DatabaseValue("sha-256")]
        SHA256,
        /// <summary>SHA-384 authentication algorithm.</summary>
        [DatabaseValue("sha-384")]
        SHA384,
        /// <summary>SHA-512 authentication algorithm.</summary>
        [DatabaseValue("sha-512")]
        SHA512
    }

    /// <summary>
    /// Encryption algorithm for SNMPv3.
    /// </summary>
    public enum CryptoAlgoEnum
    {
        /// <summary>DES encryption algorithm.</summary>
        [DatabaseValue("des")]
        DES,
        /// <summary>AES encryption algorithm.</summary>
        [DatabaseValue("aes")]
        AES,
        /// <summary>AES-192 encryption algorithm.</summary>
        [DatabaseValue("aes-192")]
        AES192,
        /// <summary>AES-256 encryption algorithm.</summary>
        [DatabaseValue("aes-256")]
        AES256,
        /// <summary>AES-256-C encryption algorithm.</summary>
        [DatabaseValue("aes-256-c")]
        AES256C
    }

    /// <summary>
    /// SNMP version.
    /// </summary>
    public enum SnmpVerEnum
    {
        /// <summary>SNMP version 1.</summary>
        [DatabaseValue("v1")]
        V1,
        /// <summary>SNMP version 2c.</summary>
        [DatabaseValue("v2c")]
        V2C,
        /// <summary>SNMP version 3.</summary>
        [DatabaseValue("v3")]
        V3
    }

    public enum DeviceTypeEnum
    {
        Undefined = 0,
        [DatabaseValue("appliance")]
        Appliance,
        [DatabaseValue("collaboration")]
        Collaboration,
        [DatabaseValue("environment")]
        Environment,
        [DatabaseValue("firewall")]
        Firewall,
        [DatabaseValue("loadbalancer")]
        LoadBalancer,
        [DatabaseValue("network")]
        Network,
        [DatabaseValue("printer")]
        Printer,
        [DatabaseValue("power")]
        Power,
        [DatabaseValue("server")]
        Server,
        [DatabaseValue("storage")]
        Storage,
        [DatabaseValue("wireless")]
        Wireless,
        [DatabaseValue("workstation")]
        Workstation
    }

    public enum StatusReasonEnum
    {
        Undefined = 0,
        [DatabaseValue("icmp")]
        ICMP,
        [DatabaseValue("snmp")]
        SNMP
    }

    public enum TransportEnum
    {
        [DatabaseValue("tcp")]
        TCP,
        [DatabaseValue("udp")]
        UDP
    }
}
