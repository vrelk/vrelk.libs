using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vrelk.Libs.JsonUtil.System.Converters;

/// <summary>  
/// A JSON converter that parses string representations of 64-bit integers (long) during deserialization  
/// and serializes long values as strings.  
/// </summary>
public class ParseInt64StringConverter : JsonConverter<long>
{
    public override bool CanConvert(Type t) => t == typeof(long);

    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (Int64.TryParse(value, out long l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToString(), options);
        return;
    }

    public static readonly ParseInt64StringConverter Singleton = new ParseInt64StringConverter();
}
