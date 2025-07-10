using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vrelk.Libs.JsonUtil.System.Converters;

/// <summary>  
/// A custom JSON converter for serializing and deserializing <see cref="DateOnly"/> values.  
/// </summary>  
/// <remarks>  
/// This converter allows you to serialize and deserialize <see cref="DateOnly"/> objects using a specified date format.  
/// If no format is provided, the default format "yyyy-MM-dd" is used.  
/// </remarks>  
/// <example>  
/// To configure this converter:  
/// <code>  
/// var options = new JsonSerializerOptions  
/// {  
///     Converters = { new DateOnlyConverter("MM/dd/yyyy") }  
/// };  
/// var json = JsonSerializer.Serialize(DateOnly.FromDateTime(DateTime.Now), options);  
/// var date = JsonSerializer.Deserialize&lt;DateOnly&gt;(json, options);  
/// </code>  
/// </example>  
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat;
    public DateOnlyConverter() : this(null) { }

    public DateOnlyConverter(string? serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
}
