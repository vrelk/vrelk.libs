using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

internal sealed class Meta
{
    [JsonProperty("start")]
    public DateTimeOffset Start { get; set; }

    [JsonProperty("step")]
    public long Step { get; set; }

    [JsonProperty("end")]
    public DateTimeOffset End { get; set; }

    [JsonProperty("rows")]
    public long Rows { get; set; }

    [JsonProperty("data_sources")]
    public List<string> DataSources { get; set; }
}