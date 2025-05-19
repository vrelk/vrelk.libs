using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vrelk.Libs.LibreNMS.DB.Types.DbObjects;

[Table("eventlog")]
public class EventLog
{
    public EventLog() { }

    [Column("event_id")]
    public int EventId { get; set; }

    [Column("device_id")]
    public int? DeviceId { get; set; }

    ///<summary>Creation date/time (Local Server Time) the alert was created.</summary>
    [Column("datetime")]
    public DateTime Datetime {get;set;}

    [Column("message")]
    public string? Message { get; set; }

    [Column("type")]
    public string? Type { get; set; }

    [Column("reference")]
    public string? Reference { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("severity")]
    public byte Severity { get; set; }
}
