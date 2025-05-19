using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

internal sealed class IcmpPerfObj
{
    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("avg")]
    public double? Average { get; set; }

    [JsonProperty("xmt")]
    public double? Transmit { get; set; }

    [JsonProperty("rcv")]
    public double? Recieve { get; set; }

    [JsonProperty("min")]
    public double? Minimum { get; set; }

    [JsonProperty("max")]
    public double? Maximum { get; set; }
}