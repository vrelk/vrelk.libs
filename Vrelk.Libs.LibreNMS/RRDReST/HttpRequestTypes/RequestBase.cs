using System;

namespace Vrelk.Libs.LibreNMS.RRDReST.HttpRequestTypes;

internal sealed class RequestBase
{
    private const string RRD_PATH_PREFIX = "librenms";

    private string Hostname { get; }
    private string FilenamePrefix { get; }
    private long? StartTime { get; }
    private long? EndTime { get; }

    /// <summary>
    /// 24 hours of data, ending at your current time-ish
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="prefix">The RRD filename prefix</param>
    public RequestBase(string hostname, string prefix)
    {
        Hostname = hostname;
        FilenamePrefix = prefix;
    }

    /// <summary>
    /// 24 hours of data, starting at the given Unix timestamp
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="prefix">The RRD filename prefix</param>
    /// <param name="start"></param>
    public RequestBase(string hostname, string prefix, long start)
    {
        Hostname = hostname;
        FilenamePrefix = prefix;
        StartTime = start;
    }

    /// <summary>
    /// 24 hours of data, starting at the given DateTimeOffset
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="prefix">The RRD filename prefix</param>
    /// <param name="start"></param>
    public RequestBase(string hostname, string prefix, DateTimeOffset start)
    {
        Hostname = hostname;
        FilenamePrefix = prefix;
        StartTime = start.ToUnixTimeSeconds();
    }

    /// <summary>
    /// Data between the 2 specified Unix timestamps
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="prefix">The RRD filename prefix</param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public RequestBase(string hostname, string prefix, long start, long end)
    {
        Hostname = hostname;
        FilenamePrefix = prefix;
        StartTime = start;
        EndTime = end;
    }

    /// <summary>
    /// Data between the 2 specified DateTimeOffsets
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="prefix">The RRD filename prefix</param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public RequestBase(string hostname, string prefix, DateTimeOffset start, DateTimeOffset end)
    {
        Hostname = hostname;
        FilenamePrefix = prefix;
        StartTime = start.ToUnixTimeSeconds();
        EndTime = end.ToUnixTimeSeconds();
    }


    /// <summary>
    /// Get the query string for the request
    /// </summary>
    /// <param name="filenameSuffix">Usually the ID number of the thing</param>
    /// <returns></returns>
    public string FormatQueryString(string filenameSuffix)
    {
        string queryString = "?rrd_path=" + Uri.EscapeDataString($"{RRD_PATH_PREFIX}/{Hostname}/{FilenamePrefix}{filenameSuffix}.rrd");

        if (StartTime.HasValue)
            queryString += $"&epoch_start_time={StartTime}";

        if (EndTime.HasValue)
            queryString += $"&epoch_end_time={EndTime}";

        return queryString;
    }

    /// <summary>
    /// Get the query string for the request. For filenames that are for the device, instead of a specific sensor id.
    /// </summary>
    /// <returns></returns>
    public string FormatQueryString()
    {
        string queryString = "?rrd_path=" + Uri.EscapeDataString($"{RRD_PATH_PREFIX}/{Hostname}/{FilenamePrefix}.rrd");

        if (StartTime.HasValue)
            queryString += $"&epoch_start_time={StartTime}";

        if (EndTime.HasValue)
            queryString += $"&epoch_end_time={EndTime}";

        return queryString;
    }
}
