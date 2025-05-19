using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vrelk.Libs.LibreNMS.RRDReST.HttpRequestTypes;

internal sealed class ResponseObj<T>
{
    //[JsonProperty("meta")]
    //public Meta Meta { get; set; }

    [JsonProperty("data")]
    public List<T> Data { get; set; }
}
