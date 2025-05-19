using Newtonsoft.Json;
using System.Collections.Generic;
using Vrelk.Libs.JsonUtil.Newtonsoft.Converters;

namespace Vrelk.Libs.LibreNMS.API.Types;

/// <summary>
/// The output from the API
/// </summary>
/// <see cref="https://docs.librenms.org/API/Logs/"/>
public partial class LogList
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("count")]
    public long Count { get; set; }

    [JsonProperty("total")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Total { get; set; }

    [JsonProperty("logs")]
    public List<LogMessage> Logs { get; set; }
}