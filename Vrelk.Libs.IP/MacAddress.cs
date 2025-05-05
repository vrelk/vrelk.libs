using System;
using System.Text.RegularExpressions;

namespace Vrelk.Libs.IP;

/// <summary>
/// A class that handles MAC addresses in various formats
/// </summary>
public class MacAddress
{
    private readonly byte[] _bytes;

    /// <summary>
    /// Format enum for MAC address representation
    /// </summary>
    public enum MacFormat
    {
        ColonUppercase,  // 00:11:22:AA:BB:CC
        ColonLowercase,  // 00:11:22:aa:bb:cc
        DottedUppercase, // 0011.22AA.BBCC
        DottedLowercase, // 0011.22aa.bbcc
        DashUppercase,   // 00-11-22-AA-BB-CC
        DashLowercase,   // 00-11-22-aa-bb-cc
        NoDelimiterUppercase, // 001122AABBCC
        NoDelimiterLowercase  // 001122aabbcc
    }

    /// <summary>
    /// Gets the MAC address in colon-separated uppercase format
    /// </summary>
    public string ColonUppercase => ToString(MacFormat.ColonUppercase);

    /// <summary>
    /// Gets the MAC address in colon-separated lowercase format
    /// </summary>
    public string ColonLowercase => ToString(MacFormat.ColonLowercase);

    /// <summary>
    /// Gets the MAC address in dotted uppercase format
    /// </summary>
    public string DottedUppercase => ToString(MacFormat.DottedUppercase);

    /// <summary>
    /// Gets the MAC address in dotted lowercase format
    /// </summary>
    public string DottedLowercase => ToString(MacFormat.DottedLowercase);

    /// <summary>
    /// Gets the MAC address in dash-separated uppercase format
    /// </summary>
    public string DashUppercase => ToString(MacFormat.DashUppercase);

    /// <summary>
    /// Gets the MAC address in dash-separated lowercase format
    /// </summary>
    public string DashLowercase => ToString(MacFormat.DashLowercase);

    /// <summary>
    /// Gets the MAC address in no-delimiter uppercase format
    /// </summary>
    public string NoDelimiterUppercase => ToString(MacFormat.NoDelimiterUppercase);

    /// <summary>
    /// Gets the MAC address in no-delimiter lowercase format
    /// </summary>
    public string NoDelimiterLowercase => ToString(MacFormat.NoDelimiterLowercase);

    /// <summary>
    /// Constructor that accepts a MAC address in any valid format
    /// </summary>
    /// <param name="macAddress">MAC address string in any supported format</param>
    /// <exception cref="ArgumentException">Thrown when the MAC address format is invalid</exception>
    public MacAddress(string macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            throw new ArgumentException("MAC address cannot be null or empty", nameof(macAddress));

        _bytes = ParseMacAddress(macAddress);
    }

    /// <summary>
    /// Parses a MAC address string in any supported format
    /// </summary>
    /// <param name="macAddress">MAC address string to parse</param>
    /// <returns>Byte array representing the MAC address</returns>
    /// <exception cref="ArgumentException">Thrown when the MAC address format is invalid</exception>
    private byte[] ParseMacAddress(string macAddress)
    {
        // Remove all common delimiters
        string cleanMac = macAddress.Replace(":", "").Replace("-", "").Replace(".", "");

        // Check if the resulting string is a valid MAC address (12 hex characters)
        if (!Regex.IsMatch(cleanMac, "^[0-9A-Fa-f]{12}$"))
            throw new ArgumentException("Invalid MAC address format", nameof(macAddress));

        // Parse the MAC address into a byte array
        byte[] bytes = new byte[6];
        for (int i = 0; i < 6; i++)
        {
            bytes[i] = Convert.ToByte(cleanMac.Substring(i * 2, 2), 16);
        }

        return bytes;
    }

    /// <summary>
    /// Returns the MAC address in the specified format
    /// </summary>
    /// <param name="format">The desired format for the MAC address</param>
    /// <returns>MAC address string in the specified format</returns>
    public string ToString(MacFormat format)
    {
        switch (format)
        {
            case MacFormat.ColonUppercase:
                return string.Join(":", GetHexStrings()).ToUpper();

            case MacFormat.ColonLowercase:
                return string.Join(":", GetHexStrings()).ToLower();

            case MacFormat.DottedUppercase:
                return string.Format("{0}{1}.{2}{3}.{4}{5}",
                    GetHexStrings()[0], GetHexStrings()[1],
                    GetHexStrings()[2], GetHexStrings()[3],
                    GetHexStrings()[4], GetHexStrings()[5]).ToUpper();

            case MacFormat.DottedLowercase:
                return string.Format("{0}{1}.{2}{3}.{4}{5}",
                    GetHexStrings()[0], GetHexStrings()[1],
                    GetHexStrings()[2], GetHexStrings()[3],
                    GetHexStrings()[4], GetHexStrings()[5]).ToLower();

            case MacFormat.DashUppercase:
                return string.Join("-", GetHexStrings()).ToUpper();

            case MacFormat.DashLowercase:
                return string.Join("-", GetHexStrings()).ToLower();

            case MacFormat.NoDelimiterUppercase:
                return string.Join("", GetHexStrings()).ToUpper();

            case MacFormat.NoDelimiterLowercase:
                return string.Join("", GetHexStrings()).ToLower();

            default:
                return string.Join(":", GetHexStrings()).ToUpper();
        }
    }

    /// <summary>
    /// Helper method to get hex strings for each byte
    /// </summary>
    /// <returns>Array of hex strings</returns>
    private string[] GetHexStrings()
    {
        string[] hexStrings = new string[6];
        for (int i = 0; i < 6; i++)
        {
            hexStrings[i] = _bytes[i].ToString("X2");
        }
        return hexStrings;
    }

    /// <summary>
    /// Returns the default string representation (colon-separated uppercase)
    /// </summary>
    /// <returns>MAC address as colon-separated uppercase string</returns>
    public override string ToString()
    {
        return ToString(MacFormat.ColonUppercase);
    }

    /// <summary>
    /// Checks if the MAC address is a broadcast address (FF:FF:FF:FF:FF:FF)
    /// </summary>
    /// <returns>True if the MAC address is a broadcast address, false otherwise</returns>
    public bool IsBroadcast()
    {
        foreach (byte b in _bytes)
        {
            if (b != 0xFF)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the MAC address is a multicast address
    /// </summary>
    /// <returns>True if the MAC address is a multicast address, false otherwise</returns>
    public bool IsMulticast()
    {
        // First byte's least significant bit indicates multicast when set to 1
        return (_bytes[0] & 0x01) == 0x01;
    }

    /// <summary>
    /// Gets the raw bytes of the MAC address
    /// </summary>
    /// <returns>Byte array representing the MAC address</returns>
    public byte[] GetBytes()
    {
        // Return a copy to prevent modification of internal state
        return (byte[])_bytes.Clone();
    }
}