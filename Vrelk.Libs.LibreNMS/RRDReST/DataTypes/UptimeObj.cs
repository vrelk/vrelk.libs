using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

internal sealed class UptimeObj
{
    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("uptime")]
    public double? Uptime { get; set; }
}
