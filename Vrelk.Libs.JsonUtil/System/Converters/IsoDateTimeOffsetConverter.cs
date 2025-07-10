using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vrelk.Libs.JsonUtil.System.Converters;

/// <summary>  
/// A custom JSON converter for serializing and deserializing <see cref="DateTimeOffset"/> objects  
/// using ISO 8601 format. This converter supports configurable date-time styles, formats,  
/// and culture settings, allowing precise control over how <see cref="DateTimeOffset"/> values  
/// are represented in JSON.  
/// </summary>  
/// <remarks>
/// You can configure the <see cref="DateTimeStyles"/>, <see cref="DateTimeFormat"/>,  
/// and <see cref="Culture"/> properties to customize the serialization and deserialization behavior.  
/// </remarks>  
/// <example>  
/// Example of setting a custom date-time format:  
/// <code>  
/// var converter = new IsoDateTimeOffsetConverter  
/// {  
///     DateTimeFormat = "yyyy-MM-dd HH:mm:ss",  
///     DateTimeStyles = DateTimeStyles.AssumeUniversal,  
///     Culture = CultureInfo.InvariantCulture  
/// };  
/// var options = new JsonSerializerOptions  
/// {  
///     Converters = { converter }  
/// };  
/// </code>  
/// </example>  
public class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

    private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

    private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
    private string? _dateTimeFormat;
    private CultureInfo? _culture;

    public DateTimeStyles DateTimeStyles
    {
        get => _dateTimeStyles;
        set => _dateTimeStyles = value;
    }

    public string? DateTimeFormat
    {
        get => _dateTimeFormat ?? string.Empty;
        set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
    }

    public CultureInfo Culture
    {
        get => _culture ?? CultureInfo.CurrentCulture;
        set => _culture = value;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        string text;

        if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
        {
            value = value.ToUniversalTime();
        }

        text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

        writer.WriteStringValue(text);
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateText = reader.GetString();

        if (string.IsNullOrEmpty(dateText) == false)
        {
            if (!string.IsNullOrEmpty(_dateTimeFormat))
            {
                return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
            }
            else
            {
                return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
            }
        }
        else
        {
            return default(DateTimeOffset);
        }
    }

    public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
}
