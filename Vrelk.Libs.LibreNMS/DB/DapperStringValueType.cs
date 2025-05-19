using System;
using System.Data;
using Dapper;

namespace Vrelk.Libs.LibreNMS.DB;


/*
 * // Register type handlers
 * Country.RegisterTypeHandler();
 * SnmpAuthLevelEnum.RegisterTypeHandler();
 * 
 * // Case-insensitive usage
 * SnmpAuthLevelEnum auth = SnmpAuthLevelEnum.NoAuthNoPriv; // Stored as "noauthnopriv"
 * SnmpAuthLevelEnum fromDb = "noAuthNoPriv"; // Works, treated as equivalent
 * Console.WriteLine(auth); // Outputs: noauthnopriv
 * Console.WriteLine(auth == fromDb); // True
 * 
 * Country country = Country.BE; // Stored as "be"
 * Country fromDbCountry = "be"; // Works, treated as equivalent
 * Console.WriteLine(country == fromDbCountry); // True
 * 
 * ================================
 * 
 * // Example usage for Country
 * public struct Country : IEquatable<Country>
 * {
 *     private StringValueType<Country> _value;
 * 
 *     public static Country BE => "BE";
 *     public static Country NL => "NL";
 *     public static Country DE => "DE";
 *     public static Country GB => "GB";
 * 
 *     private Country(string value)
 *     {
 *         _value = value;
 *     }
 * 
 *     public static implicit operator Country(string value) => new Country(value);
 *     public static implicit operator string(Country country) => (string)country._value;
 *     public override string ToString() => _value.ToString();
 *     public override bool Equals(object obj) => obj is Country other && Equals(other);
 *     public bool Equals(Country other) => _value.Equals(other._value);
 *     public override int GetHashCode() => _value.GetHashCode();
 * 
 *     public static void RegisterTypeHandler() => StringValueType<Country>.RegisterTypeHandler();
 * }
 * 
 * ================================
 * 
 * public struct SnmpAuthLevelEnum : IEquatable<SnmpAuthLevelEnum>
 * {
 *     private StringValueType<SnmpAuthLevelEnum> _value;
 * 
 *     public static SnmpAuthLevelEnum NoAuthNoPriv => "NoAuthNoPriv";
 *     public static SnmpAuthLevelEnum AuthNoPriv => "AuthNoPriv";
 *     public static SnmpAuthLevelEnum AuthPriv => "AuthPriv";
 * 
 *     private SnmpAuthLevelEnum(string value)
 *     {
 *         _value = value;
 *     }
 * 
 *     public static implicit operator SnmpAuthLevelEnum(string value) => new SnmpAuthLevelEnum(value);
 *     public static implicit operator string(SnmpAuthLevelEnum auth) => (string)auth._value;
 *     public override string ToString() => _value.ToString();
 *     public override bool Equals(object obj) => obj is SnmpAuthLevelEnum other && Equals(other);
 *     public bool Equals(SnmpAuthLevelEnum other) => _value.Equals(other._value);
 *     public override int GetHashCode() => _value.GetHashCode();
 * 
 *     public static void RegisterTypeHandler() => StringValueType<SnmpAuthLevelEnum>.RegisterTypeHandler();
 * }
 */



/// <summary>
/// A generic struct for case-insensitive string-based value types, providing implicit conversions to and from strings.
/// </summary>
/// <typeparam name="T">The struct type, used to ensure type safety.</typeparam>
public struct StringValueType<T> where T : struct
{
    private string value; // Stored in lowercase for consistency

    /// <summary>
    /// Initializes a new instance of <see cref="StringValueType{T}"/> with the specified string value.
    /// </summary>
    /// <param name="value">The string value, which will be stored in lowercase.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    private StringValueType(string value)
    {
        this.value = value?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Implicitly converts a string to a <see cref="StringValueType{T}"/>.
    /// </summary>
    /// <param name="value">The string value to convert.</param>
    /// <returns>A new <see cref="StringValueType{T}"/> instance with the normalized (lowercase) value.</returns>
    public static implicit operator StringValueType<T>(string value)
    {
        return new StringValueType<T>(value);
    }

    /// <summary>
    /// Implicitly converts a <see cref="StringValueType{T}"/> to a string.
    /// </summary>
    /// <param name="type">The <see cref="StringValueType{T}"/> instance to convert.</param>
    /// <returns>The stored string value (in lowercase).</returns>
    public static implicit operator string(StringValueType<T> type)
    {
        return type.value;
    }

    /// <summary>
    /// Returns the string representation of the value.
    /// </summary>
    /// <returns>The stored string value (in lowercase).</returns>
    public override string ToString() => value;

    /// <summary>
    /// Determines whether the specified object is equal to the current instance, ignoring case.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is a <see cref="StringValueType{T}"/> with an equal value (case-insensitive); otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) =>
        obj is StringValueType<T> other && string.Equals(value, other.value, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Returns the hash code for the value, computed in a case-insensitive manner.
    /// </summary>
    /// <returns>A hash code for the stored string value.</returns>
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(value);

    /// <summary>
    /// Registers a Dapper type handler for this <see cref="StringValueType{T}"/> type.
    /// </summary>
    /// <remarks>
    /// Call this method once at application startup to enable Dapper to map this type to and from database strings.
    /// </remarks>
    public static void RegisterTypeHandler()
    {
        SqlMapper.AddTypeHandler(typeof(StringValueType<T>), new StringValueTypeHandler<T>());
    }
}

/// <summary>
/// A generic Dapper type handler for mapping <see cref="StringValueType{T}"/> to and from database strings.
/// </summary>
/// <typeparam name="T">The struct type associated with the <see cref="StringValueType{T}"/>.</typeparam>
public class StringValueTypeHandler<T> : SqlMapper.ITypeHandler where T : struct
{
    /// <summary>
    /// Parses a database value into a <see cref="StringValueType{T}"/>.
    /// </summary>
    /// <param name="destinationType">The type to parse into.</param>
    /// <param name="value">The database value to parse.</param>
    /// <returns>A <see cref="StringValueType{T}"/> if the destination type matches and the value is a string; otherwise, <c>null</c>.</returns>
    public object Parse(Type destinationType, object value)
    {
        if (destinationType == typeof(StringValueType<T>) && value is string str)
            return (StringValueType<T>)str;
        return null;
    }

    /// <summary>
    /// Sets the value of a database parameter for a <see cref="StringValueType{T}"/>.
    /// </summary>
    /// <param name="parameter">The database parameter to set.</param>
    /// <param name="value">The <see cref="StringValueType{T}"/> value to set.</param>
    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = (string)(StringValueType<T>)value;
    }
}
