using Newtonsoft.Json;
using System;
using Vrelk.Libs.Common;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

/// <summary>
/// All values are per second == ([new value] - [last value]) / [seconds since last poll]
/// From the file `port-id___.rrd`
/// </summary>
internal sealed class PortObj
{
    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("inoctets")]
    public double? InOctets { get; set; }
    public DataSizeConverter InputData => DataSizeConverter.FromBit(InOctets ?? 0);

    [JsonProperty("outoctets")]
    public double? OutOctets { get; set; }
    public DataSizeConverter OutputData => DataSizeConverter.FromBit(OutOctets ?? 0);

    [JsonProperty("inerrors")]
    public double? InErrors { get; set; }

    [JsonProperty("outerrors")]
    public double? OutErrors { get; set; }

    [JsonProperty("inucastpkts")]
    public double? InUcastPkts { get; set; }

    [JsonProperty("outucastpkts")]
    public double? OutUcastPkts { get; set; }

    [JsonProperty("innucastpkts")]
    public double? InNucastPkts { get; set; }

    [JsonProperty("outnucastpkts")]
    public double? OutNucastPkts { get; set; }

    [JsonProperty("indiscards")]
    public double? InDiscards { get; set; }

    [JsonProperty("outdiscards")]
    public double? OutDiscards { get; set; }

    [JsonProperty("inunknownprotos")]
    public double? InUnknownProtos { get; set; }

    [JsonProperty("inbroadcastpkts")]
    public double? InBroadcastPkts { get; set; }

    [JsonProperty("outbroadcastpkts")]
    public double? OutBroadcastPkts { get; set; }

    [JsonProperty("inmulticastpkts")]
    public double? InMulticastPkts { get; set; }

    [JsonProperty("outmulticastpkts")]
    public double? OutMulticastPkts { get; set; }
}