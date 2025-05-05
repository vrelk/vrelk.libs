using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Vrelk.Libs.Types.Net;

/// <summary>
/// Represents an IP subnet and provides methods to work with subnet-related operations.
/// </summary>
public class Subnet
{
    private uint _networkAddress;
    private uint _subnetMask;

    #region Constructors

    /// <summary>
    /// Creates a subnet from CIDR notation (e.g., "192.168.0.0/24")
    /// </summary>
    /// <param name="cidrNotation">The subnet in CIDR notation</param>
    public Subnet(string cidrNotation)
    {
        if (string.IsNullOrWhiteSpace(cidrNotation))
            throw new ArgumentNullException(nameof(cidrNotation));

        // Check for CIDR notation format
        var cidrMatch = Regex.Match(cidrNotation, @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})/(\d{1,2})$");
        if (cidrMatch.Success)
        {
            string ipPart = cidrMatch.Groups[1].Value;
            int prefixLength = int.Parse(cidrMatch.Groups[2].Value);

            if (prefixLength < 0 || prefixLength > 32)
                throw new ArgumentException("Invalid prefix length. Must be between 0 and 32.");

            _networkAddress = IpToUint(ipPart);
            _subnetMask = PrefixLengthToSubnetMask(prefixLength);
        }
        // Check for IP/Netmask format
        else
        {
            var maskMatch = Regex.Match(cidrNotation, @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})/(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$");
            if (maskMatch.Success)
            {
                string ipPart = maskMatch.Groups[1].Value;
                string maskPart = maskMatch.Groups[2].Value;

                _networkAddress = IpToUint(ipPart);
                _subnetMask = IpToUint(maskPart);
            }
            else
            {
                throw new ArgumentException("Invalid subnet format. Expected format: '192.168.0.0/24' or '192.168.0.0/255.255.255.0'");
            }
        }

        // Ensure network address is valid by applying the mask
        _networkAddress &= _subnetMask;
    }

    /// <summary>
    /// Creates a subnet from an IP address and subnet mask in dotted decimal notation
    /// </summary>
    /// <param name="ipAddress">The IP address (e.g., "192.168.0.0")</param>
    /// <param name="subnetMask">The subnet mask (e.g., "255.255.255.0")</param>
    public Subnet(string ipAddress, string subnetMask)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentNullException(nameof(ipAddress));
        if (string.IsNullOrWhiteSpace(subnetMask))
            throw new ArgumentNullException(nameof(subnetMask));

        _networkAddress = IpToUint(ipAddress);
        _subnetMask = IpToUint(subnetMask);

        // Ensure network address is valid by applying the mask
        _networkAddress &= _subnetMask;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the network address of the subnet in dotted decimal notation
    /// </summary>
    public string NetworkAddress => UintToIp(_networkAddress);

    /// <summary>
    /// Gets the broadcast address of the subnet in dotted decimal notation
    /// </summary>
    public string BroadcastAddress => UintToIp(_networkAddress | ~_subnetMask);

    /// <summary>
    /// Gets the subnet mask in dotted decimal notation
    /// </summary>
    public string SubnetMask => UintToIp(_subnetMask);

    /// <summary>
    /// Gets the CIDR prefix length (e.g., /24)
    /// </summary>
    public int PrefixLength
    {
        get
        {
            uint mask = _subnetMask;
            int length = 0;

            while (mask > 0)
            {
                if ((mask & 1) == 1)
                    length++;
                mask >>= 1;
            }

            return length;
        }
    }

    /// <summary>
    /// Gets the first usable IP address in the subnet
    /// </summary>
    public string FirstUsableIp
    {
        get
        {
            // If subnet is a /31 or /32, the network address is usable
            if (PrefixLength >= 31)
                return NetworkAddress;

            return UintToIp(_networkAddress + 1);
        }
    }

    /// <summary>
    /// Gets the last usable IP address in the subnet
    /// </summary>
    public string LastUsableIp
    {
        get
        {
            uint broadcastAddr = _networkAddress | ~_subnetMask;

            // If subnet is a /31 or /32, the broadcast address is usable
            if (PrefixLength >= 31)
                return UintToIp(broadcastAddr);

            return UintToIp(broadcastAddr - 1);
        }
    }

    /// <summary>
    /// Gets the total number of usable IP addresses in the subnet
    /// </summary>
    public uint UsableIpAddressCount
    {
        get
        {
            // Special cases for /31 and /32
            if (PrefixLength == 31)
                return 2;
            if (PrefixLength == 32)
                return 1;

            uint hostBits = 32 - (uint)PrefixLength;
            return (uint)Math.Pow(2, hostBits) - 2;
        }
    }

    /// <summary>
    /// Gets the subnet in CIDR notation
    /// </summary>
    public string CidrNotation => $"{NetworkAddress}/{PrefixLength}";

    #endregion

    #region Methods

    /// <summary>
    /// Determines whether the specified IP address is within this subnet
    /// </summary>
    /// <param name="ipAddress">The IP address to check</param>
    /// <returns>true if the IP address is within this subnet; otherwise, false</returns>
    public bool Contains(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentNullException(nameof(ipAddress));

        uint ip = IpToUint(ipAddress);
        uint networkAddrOfIp = ip & _subnetMask;

        return networkAddrOfIp == _networkAddress;
    }

    /// <summary>
    /// Returns a string that represents the current subnet
    /// </summary>
    /// <returns>A string in CIDR notation</returns>
    public override string ToString()
    {
        return CidrNotation;
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Converts an IP address in dotted decimal notation to an unsigned integer
    /// </summary>
    private static uint IpToUint(string ipAddress)
    {
        if (!IPAddress.TryParse(ipAddress, out IPAddress? ip))
            throw new ArgumentException($"Invalid IP address format: {ipAddress}");

        byte[] bytes = ip.GetAddressBytes();

        // Convert from network order (big endian)
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// Converts an unsigned integer to an IP address in dotted decimal notation
    /// </summary>
    private static string UintToIp(uint ipAddress)
    {
        byte[] bytes = BitConverter.GetBytes(ipAddress);

        // Convert to network order (big endian)
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return new IPAddress(bytes).ToString();
    }

    /// <summary>
    /// Converts a CIDR prefix length to a subnet mask
    /// </summary>
    private static uint PrefixLengthToSubnetMask(int prefixLength)
    {
        return prefixLength == 0 ? 0 : ~(uint.MaxValue >> prefixLength);
    }

    #endregion
}