namespace Vrelk.Libs.LibreNMS.API.Types
{
    using System;
    using Newtonsoft.Json;
    using Vrelk.Libs.JsonUtil.Newtonsoft.Converters;

    public partial class LogMessage
    {
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("sysName")]
        public string SysName { get; set; }

        [JsonProperty("event_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long EventId { get; set; }

        [JsonProperty("host")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Host { get; set; }

        [JsonProperty("device_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DeviceId { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset Datetime { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("severity")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Severity { get; set; }
    }
}
