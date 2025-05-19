using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.HttpRequestTypes;

internal static class QueryGenerator
{
    public static string PortQuery(string hostname, string portId) => new RequestBase(hostname, "port-id").FormatQueryString(portId);
    public static string PortQuery(string hostname, string portId, long startTime) => new RequestBase(hostname, "port-id", startTime).FormatQueryString(portId);
    public static string PortQuery(string hostname, string portId, long startTime, long endTime) => new RequestBase(hostname, "port-id", startTime, endTime).FormatQueryString(portId);
    public static string PortQuery(string hostname, string portId, DateTimeOffset startTime) => new RequestBase(hostname, "port-id", startTime).FormatQueryString(portId);
    public static string PortQuery(string hostname, string portId, DateTimeOffset startTime, DateTimeOffset endTime) => new RequestBase(hostname, "port-id", startTime, endTime).FormatQueryString(portId);
}
