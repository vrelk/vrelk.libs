using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

namespace Vrelk.Libs.JsonUtil.Newtonsoft;

/// <summary>
/// General settings for Newtonsoft.Json serialization and deserialization.
/// </summary>
public static class Settings
{
    /// <summary>
    /// Customizes Newtonsoft.Json settings to ignore metadata properties, disable automatic date parsing,
    /// and enforce UTC handling for DateTime values.
    /// </summary>
    /// <remarks>
    /// These settings are useful when working with APIs or data sources where metadata properties are irrelevant 
    /// or should be ignored, and where strict control over date and time handling is required. 
    /// 
    /// Differences from defaults:
    /// - MetadataPropertyHandling.Ignore: Prevents processing of metadata properties like `$type`, which are 
    ///   often unnecessary in many scenarios.
    /// - DateParseHandling.None: Disables automatic parsing of date strings into DateTime objects, allowing 
    ///   developers to handle date parsing explicitly.
    /// - IsoDateTimeConverter with DateTimeStyles.AssumeUniversal: Ensures that all DateTime values are treated 
    ///   as UTC, avoiding potential issues with time zone conversions.
    /// 
    /// These settings provide greater control and predictability when serializing and deserializing JSON data.
    /// </remarks>
    public static readonly JsonSerializerSettings QuickTypeDefaults = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
               {
                   new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
               },
    };
}
