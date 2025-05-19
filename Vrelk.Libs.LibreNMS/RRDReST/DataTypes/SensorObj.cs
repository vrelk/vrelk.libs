using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

internal sealed class SensorObj
{
    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("sensor")]
    public double? Sensor { get; set; }
}
