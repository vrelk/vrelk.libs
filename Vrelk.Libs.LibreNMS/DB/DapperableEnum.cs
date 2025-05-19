using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Vrelk.Libs.LibreNMS.DB.DapperExtensions;


/*
 * public class TestModel
 * {
 * 	internal DapperableEnum<TestEnum> TestEnumString { get; set;}
 * 	public TestEnum? TestEnum => TestEnumString.Value;
 * }
 * 
 * public enum TestEnum
 * {
 * 	[DatabaseValue("STRING_ONE")]
 * 	One = 1,
 * 	[DatabaseValue("STRING_TWO")]
 * 	Two = 2,
 * 	Other = 3
 * }
 */


internal enum TestEnum
{
    Value0,
    [DatabaseValue("DB_Value1")]
    Value1,
    [DatabaseValue("DB_Value2")]
    Value2
}

public readonly struct DapperableEnum<TEnum> where TEnum : Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public TEnum Value { get; }

    static DapperableEnum()
    {
        SqlMapper.AddTypeHandler(typeof(DapperableEnum<TEnum>), new DapperableEnumHandler<TEnum>());
    }

    public DapperableEnum(TEnum value)
    {
        Value = value;
    }
    public DapperableEnum(string description)
    {
        Value = description.GetValueFromDatabaseValueAttribute<TEnum>();
    }

    public static implicit operator DapperableEnum<TEnum>(TEnum v) => new DapperableEnum<TEnum>(v);
    public static implicit operator TEnum(DapperableEnum<TEnum> v) => v.Value;
    public static implicit operator DapperableEnum<TEnum>(string s) => new DapperableEnum<TEnum>(s);
    public override string ToString() => Value.ToString();
}

public class DapperableEnumHandler<TEnum> : SqlMapper.ITypeHandler
        where TEnum : Enum
{
    public object Parse(Type destinationType, object value)
    {
        if (destinationType == typeof(DapperableEnum<TEnum>))
        {
            return new DapperableEnum<TEnum>((string)value);
        }
        throw new InvalidCastException($"Can't parse string value {value} into enum type {typeof(TEnum).Name}");
    }

    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = ((DapperableEnum<TEnum>)value).Value.GetDatabaseAttribute();
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class DatabaseValueAttribute : Attribute
{
    public string Value { get; }
    public DatabaseValueAttribute(string value) => this.Value = value;
}

public static class EnumExtensions
{
    public static bool FailOnNotExisting { get; set; }

    public static string GetDatabaseAttribute(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        return Attribute.GetCustomAttribute(field, typeof(DatabaseValueAttribute)) is DatabaseValueAttribute attribute ? attribute.Value : value.ToString();
    }

    public static T? GetValueFromDatabaseValueAttribute<T>(this string value) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
            typeof(DatabaseValueAttribute)) is DatabaseValueAttribute attribute)
            {
                if (attribute.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == value)
                    return (T)field.GetValue(null);
            }
        }

        return !FailOnNotExisting ? default : throw new ArgumentException("Not found.", nameof(value));
    }
}
