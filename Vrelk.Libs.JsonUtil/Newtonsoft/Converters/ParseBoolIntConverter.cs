using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.JsonUtil.Newtonsoft.Converters;

/// <summary>
/// A JSON converter that parses integer representations of boolean values (1 for true, 0 for false) during deserialization
/// </summary>
public class ParseBoolIntConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<int>(reader);
        return value == 1;
        throw new Exception("Cannot unmarshal type bool");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (bool)untypedValue;
        var boolString = value ? 1 : 0;
        serializer.Serialize(writer, boolString);
        return;
    }

    public static readonly ParseBoolIntConverter Singleton = new ParseBoolIntConverter();
}
