using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.JsonUtil.Newtonsoft.Converters;

/// <summary>
/// Converter for parsing boolean values from strings (quoted true/false).
/// </summary>
internal class ParseBoolStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        bool b;
        if (Boolean.TryParse(value, out b))
        {
            return b;
        }
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
        var boolString = value ? "true" : "false";
        serializer.Serialize(writer, boolString);
        return;
    }

    public static readonly ParseBoolStringConverter Singleton = new ParseBoolStringConverter();
}
