using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.DataTypes;

/// <summary>
/// From the file `netstats-snmp.rrd`
/// </summary>
internal sealed class NetStatsSnmpObj
{
    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("snmpinpkts")]
    public double? SnmpInPkts { get; set; }

    [JsonProperty("snmpoutpkts")]
    public double? SnmpOutPkts { get; set; }

    [JsonProperty("snmpinbadversions")]
    public double? SnmpInBadVersions { get; set; }

    [JsonProperty("snmpinbadcommunityn")]
    public double? SnmpInBadCommunityN { get; set; }

    [JsonProperty("snmpinbadcommunityu")]
    public double? SnmpInBadCommunityU { get; set; }

    [JsonProperty("snmpinasnparseerrs")]
    public double? SnmpInAsnParseErrs { get; set; }

    [JsonProperty("snmpintoobigs")]
    public double? SnmpInTooBigs { get; set; }

    [JsonProperty("snmpinnosuchnames")]
    public double? SnmpInNoSuchNames { get; set; }

    [JsonProperty("snmpinbadvalues")]
    public double? SnmpInBadValues { get; set; }

    [JsonProperty("snmpinreadonlys")]
    public double? SnmpInReadOnlys { get; set; }

    [JsonProperty("snmpingenerrs")]
    public double? SnmpInGenErrs { get; set; }

    [JsonProperty("snmpintotalreqvars")]
    public double? SnmpInTotalReqVars { get; set; }

    [JsonProperty("snmpintotalsetvars")]
    public double? SnmpInTotalSetVars { get; set; }

    [JsonProperty("snmpingetrequests")]
    public double? SnmpInGetRequests { get; set; }

    [JsonProperty("snmpingetnexts")]
    public double? SnmpInGetNexts { get; set; }

    [JsonProperty("snmpinsetrequests")]
    public double? SnmpInSetRequests { get; set; }

    [JsonProperty("snmpingetresponses")]
    public double? SnmpInGetResponses { get; set; }

    [JsonProperty("snmpintraps")]
    public double? SnmpInTraps { get; set; }

    [JsonProperty("snmpouttoobigs")]
    public double? SnmpOutTooBigs { get; set; }

    [JsonProperty("snmpoutnosuchnames")]
    public double? SnmpOutNoSuchNames { get; set; }

    [JsonProperty("snmpoutbadvalues")]
    public double? SnmpOutBadValues { get; set; }

    [JsonProperty("snmpoutgenerrs")]
    public double? SnmpOutGenErrs { get; set; }

    [JsonProperty("snmpoutgetrequests")]
    public double? SnmpOutGetRequests { get; set; }

    [JsonProperty("snmpoutgetnexts")]
    public double? SnmpOutGetNexts { get; set; }

    [JsonProperty("snmpoutsetrequests")]
    public double? SnmpOutSetRequests { get; set; }

    [JsonProperty("snmpoutgetresponses")]
    public double? SnmpOutGetResponses { get; set; }

    [JsonProperty("snmpouttraps")]
    public double? SnmpOutTraps { get; set; }

    [JsonProperty("snmpsilentdrops")]
    public double? SnmpSilentDrops { get; set; }

    [JsonProperty("snmpproxydrops")]
    public double? SnmpProxyDrops { get; set; }
}