using Newtonsoft.Json;
using System;

namespace Vrelk.Libs.JsonUtil.Newtonsoft.Converters;

/// <summary>
/// Converter for parsing integer values from strings (quoted integers).
/// </summary>
internal class ParseInt32StringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        int l;
        if (Int32.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type int");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (int)untypedValue;
        serializer.Serialize(writer, value.ToString());
        return;
    }

    public static readonly ParseInt32StringConverter Singleton = new ParseInt32StringConverter();
}
