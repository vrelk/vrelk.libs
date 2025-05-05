# Subnet Class Documentation

## Overview

The `Subnet` class provides a robust implementation for working with IP subnets in C#. It offers convenient methods and properties for subnet calculations, including network address determination, broadcast address calculation, and IP address containment checks.

## Namespace

```csharp
namespace Vrelk.Libs.Types.Net;
```

## Class Definition

```csharp
public class Subnet
```

## Constructors

The `Subnet` class provides multiple constructors to accommodate different input formats:

### CIDR Notation

```csharp
public Subnet(string cidrNotation)
```

Creates a subnet from CIDR notation.

**Parameters:**
- `cidrNotation`: A string in the format "192.168.0.0/24" where the number after the slash represents the prefix length.

**Example:**
```csharp
var subnet = new Subnet("192.168.0.0/24");
```

**Exceptions:**
- `ArgumentNullException`: Thrown when the input is null or empty.
- `ArgumentException`: Thrown when the input format is invalid or the prefix length is out of range (0-32).

### IP with Subnet Mask (String Format)

```csharp
public Subnet(string ipAddress, string subnetMask)
```

Creates a subnet from an IP address and subnet mask in dotted decimal notation.

**Parameters:**
- `ipAddress`: The IP address in dotted decimal notation (e.g., "192.168.0.0").
- `subnetMask`: The subnet mask in dotted decimal notation (e.g., "255.255.255.0").

**Example:**
```csharp
var subnet = new Subnet("172.16.0.0", "255.255.255.240");
```

**Exceptions:**
- `ArgumentNullException`: Thrown when either parameter is null or empty.
- `ArgumentException`: Thrown when either parameter has an invalid IP address format.

## Properties

### NetworkAddress

```csharp
public string NetworkAddress { get; }
```

Gets the network address of the subnet in dotted decimal notation.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
Console.WriteLine(subnet.NetworkAddress); // Outputs: 192.168.1.0
```

### BroadcastAddress

```csharp
public string BroadcastAddress { get; }
```

Gets the broadcast address of the subnet in dotted decimal notation.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
Console.WriteLine(subnet.BroadcastAddress); // Outputs: 192.168.1.255
```

### SubnetMask

```csharp
public string SubnetMask { get; }
```

Gets the subnet mask in dotted decimal notation.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
Console.WriteLine(subnet.SubnetMask); // Outputs: 255.255.255.0
```

### PrefixLength

```csharp
public int PrefixLength { get; }
```

Gets the CIDR prefix length (e.g., 24 for a /24 subnet).

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0", "255.255.255.0");
Console.WriteLine(subnet.PrefixLength); // Outputs: 24
```

### FirstUsableIp

```csharp
public string FirstUsableIp { get; }
```

Gets the first usable IP address in the subnet.

**Note:**
- For /31 and /32 subnets, the network address is considered usable.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
Console.WriteLine(subnet.FirstUsableIp); // Outputs: 192.168.1.1
```

### LastUsableIp

```csharp
public string LastUsableIp { get; }
```

Gets the last usable IP address in the subnet.

**Note:**
- For /31 and /32 subnets, the broadcast address is considered usable.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
Console.WriteLine(subnet.LastUsableIp); // Outputs: 192.168.1.254
```

### UsableIpAddressCount

```csharp
public uint UsableIpAddressCount { get; }
```

Gets the total number of usable IP addresses in the subnet.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
Console.WriteLine(subnet.UsableIpAddressCount); // Outputs: 254
```

### CidrNotation

```csharp
public string CidrNotation { get; }
```

Gets the subnet in CIDR notation.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0", "255.255.255.0");
Console.WriteLine(subnet.CidrNotation); // Outputs: 192.168.1.0/24
```

## Methods

### Contains

```csharp
public bool Contains(string ipAddress)
```

Determines whether the specified IP address is within this subnet.

**Parameters:**
- `ipAddress`: The IP address to check, in dotted decimal notation.

**Returns:**
- `true` if the IP address is within this subnet; otherwise, `false`.

**Exceptions:**
- `ArgumentNullException`: Thrown when the input is null or empty.
- `ArgumentException`: Thrown when the input has an invalid IP address format.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/24");
bool isInSubnet = subnet.Contains("192.168.1.100"); // Returns true
bool notInSubnet = subnet.Contains("192.168.2.100"); // Returns false
```

### ToString

```csharp
public override string ToString()
```

Returns a string that represents the current subnet in CIDR notation.

**Returns:**
- A string in CIDR notation representing this subnet.

**Example:**
```csharp
var subnet = new Subnet("192.168.1.0/255.255.255.0");
Console.WriteLine(subnet.ToString()); // Outputs: 192.168.1.0/24
```

## Usage Examples

### Basic Usage

```csharp
using System;
using Vrelk.Libs.Types.Net;

class Program
{
    static void Main()
    {
        // Create a subnet using CIDR notation
        var subnet1 = new Subnet("192.168.1.0/24");
        
        // Create a subnet using IP and mask separately
        var subnet2 = new Subnet("10.0.0.0", "255.255.0.0");
        
        // Display subnet information
        Console.WriteLine($"Subnet 1: {subnet1}");
        Console.WriteLine($"Network Address: {subnet1.NetworkAddress}");
        Console.WriteLine($"Broadcast Address: {subnet1.BroadcastAddress}");
        Console.WriteLine($"First Usable IP: {subnet1.FirstUsableIp}");
        Console.WriteLine($"Last Usable IP: {subnet1.LastUsableIp}");
        Console.WriteLine($"Usable IP Count: {subnet1.UsableIpAddressCount}");
        
        // Check if an IP is in the subnet
        string testIp = "192.168.1.100";
        Console.WriteLine($"Is {testIp} in the subnet? {subnet1.Contains(testIp)}");
    }
}
```

### Working with Different Subnet Formats

```csharp
// Different ways to create the same /24 subnet
var subnet1 = new Subnet("192.168.1.0/24");
var subnet2 = new Subnet("192.168.1.0/255.255.255.0");
var subnet3 = new Subnet("192.168.1.0", "255.255.255.0");

// All these will print the same information
Console.WriteLine($"Subnet 1: {subnet1.CidrNotation}");
Console.WriteLine($"Subnet 2: {subnet2.CidrNotation}");
Console.WriteLine($"Subnet 3: {subnet3.CidrNotation}");
```

### Special Cases

```csharp
// /32 subnet (single IP)
var singleIp = new Subnet("192.168.1.1/32");
Console.WriteLine($"Network: {singleIp.NetworkAddress}");
Console.WriteLine($"Broadcast: {singleIp.BroadcastAddress}");
Console.WriteLine($"First IP: {singleIp.FirstUsableIp}");
Console.WriteLine($"Last IP: {singleIp.LastUsableIp}");
Console.WriteLine($"Usable IPs: {singleIp.UsableIpAddressCount}");

// /31 subnet (point-to-point link with 2 IPs)
var p2pLink = new Subnet("192.168.1.0/31");
Console.WriteLine($"Network: {p2pLink.NetworkAddress}");
Console.WriteLine($"Broadcast: {p2pLink.BroadcastAddress}");
Console.WriteLine($"First IP: {p2pLink.FirstUsableIp}");
Console.WriteLine($"Last IP: {p2pLink.LastUsableIp}");
Console.WriteLine($"Usable IPs: {p2pLink.UsableIpAddressCount}");
```

## Implementation Notes

- The class handles special cases for /31 and /32 subnets according to RFC 3021.
- IP addresses are stored internally as unsigned integers for efficient calculations.
- All input parameters are validated to ensure they contain valid IP addresses and subnet masks.
- The network address is automatically aligned to the proper subnet boundary based on the subnet mask.
