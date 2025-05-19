using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

internal sealed class StorageObj
{
    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("used")]
    public double? Used { get; set; }

    [JsonProperty("free")]
    public double? Free { get; set; }
}
