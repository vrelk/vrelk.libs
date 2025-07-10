using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vrelk.Libs.JsonUtil.System.Converters;

/// <summary>  
/// A custom JSON converter for serializing and deserializing <see cref="TimeOnly"/> values.  
/// Supports configurable serialization formats.  
/// </summary>  
/// <remarks>  
/// By default, the converter uses the "HH:mm:ss.fff" format for serialization.  
/// You can specify a custom format by passing it to the constructor.  
/// </remarks>  
/// <example>  
/// To use this converter with a custom format:  
/// <code>  
/// var options = new JsonSerializerOptions  
/// {  
///     Converters = { new TimeOnlyConverter("HH:mm") }  
/// };  
/// var json = JsonSerializer.Serialize(TimeOnly.FromDateTime(DateTime.Now), options);  
/// </code>  
/// </example>  
public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    private readonly string serializationFormat;

    public TimeOnlyConverter() : this(null) { }

    public TimeOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
    }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TimeOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
}
