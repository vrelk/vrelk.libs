# MacAddress Class Documentation

## Overview

The `MacAddress` class provides comprehensive functionality for handling MAC (Media Access Control) addresses in various formats. It allows parsing MAC addresses from different input formats and retrieving them in any desired format.

## Namespace

```csharp
namespace Vrelk.Libs.Types.Net;
```

## Class Definition

```csharp
public class MacAddress
```

## Constructor

### `MacAddress(string macAddress)`

Creates a new instance of the `MacAddress` class by parsing the provided MAC address string.

**Parameters:**
- `macAddress`: A string containing a MAC address in any supported format.

**Throws:**
- `ArgumentException`: If the MAC address is null, empty, or in an invalid format.

**Example:**
```csharp
// Create a MacAddress from different formats
var mac1 = new MacAddress("00:11:22:AA:BB:CC"); // Colon-separated
var mac2 = new MacAddress("00-11-22-AA-BB-CC"); // Dash-separated
var mac3 = new MacAddress("0011.22AA.BBCC");    // Dotted format
var mac4 = new MacAddress("001122AABBCC");      // No delimiter
```

## Properties

The `MacAddress` class provides convenient read-only properties for accessing the MAC address in various formats:

| Property | Description | Example |
|----------|-------------|---------|
| `ColonUppercase` | Colon-separated format with uppercase letters | `"00:11:22:AA:BB:CC"` |
| `ColonLowercase` | Colon-separated format with lowercase letters | `"00:11:22:aa:bb:cc"` |
| `DottedUppercase` | Dotted format with uppercase letters | `"0011.22AA.BBCC"` |
| `DottedLowercase` | Dotted format with lowercase letters | `"0011.22aa.bbcc"` |
| `DashUppercase` | Dash-separated format with uppercase letters | `"00-11-22-AA-BB-CC"` |
| `DashLowercase` | Dash-separated format with lowercase letters | `"00-11-22-aa-bb-cc"` |
| `NoDelimiterUppercase` | No delimiter format with uppercase letters | `"001122AABBCC"` |
| `NoDelimiterLowercase` | No delimiter format with lowercase letters | `"001122aabbcc"` |

**Example:**
```csharp
var mac = new MacAddress("00:11:22:AA:BB:CC");

// Access directly via properties
string colonUpper = mac.ColonUppercase;      // "00:11:22:AA:BB:CC"
string dashLower = mac.DashLowercase;        // "00-11-22-aa-bb-cc"
string noDelimUpper = mac.NoDelimiterUppercase; // "001122AABBCC"
```

## Format Enum

The `MacAddress` class defines an enum for specifying the desired output format:

```csharp
public enum MacFormat
{
    ColonUppercase,      // 00:11:22:AA:BB:CC
    ColonLowercase,      // 00:11:22:aa:bb:cc
    DottedUppercase,     // 0011.22AA.BBCC
    DottedLowercase,     // 0011.22aa.bbcc
    DashUppercase,       // 00-11-22-AA-BB-CC
    DashLowercase,       // 00-11-22-aa-bb-cc
    NoDelimiterUppercase, // 001122AABBCC
    NoDelimiterLowercase  // 001122aabbcc
}
```

## Methods

### `ToString(MacFormat format)`

Returns the MAC address as a string in the specified format.

**Parameters:**
- `format`: The desired format for the MAC address as a `MacFormat` enum value.

**Returns:**
- A string representation of the MAC address in the specified format.

**Example:**
```csharp
var mac = new MacAddress("00:11:22:AA:BB:CC");
string formatted = mac.ToString(MacAddress.MacFormat.DashUppercase); // "00-11-22-AA-BB-CC"
```

### `ToString()`

Returns the default string representation of the MAC address (colon-separated uppercase).

**Returns:**
- The MAC address as a colon-separated uppercase string.

**Example:**
```csharp
var mac = new MacAddress("001122aabbcc");
string defaultFormat = mac.ToString(); // "00:11:22:AA:BB:CC"
```

### `IsBroadcast()`

Determines whether the MAC address is a broadcast address (FF:FF:FF:FF:FF:FF).

**Returns:**
- `true` if the MAC address is a broadcast address; otherwise, `false`.

**Example:**
```csharp
var mac = new MacAddress("FF:FF:FF:FF:FF:FF");
bool isBroadcast = mac.IsBroadcast(); // true
```

### `IsMulticast()`

Determines whether the MAC address is a multicast address.

**Returns:**
- `true` if the MAC address is a multicast address; otherwise, `false`.

**Example:**
```csharp
var mac = new MacAddress("01:00:5E:00:00:01");
bool isMulticast = mac.IsMulticast(); // true
```

### `GetBytes()`

Returns the raw bytes of the MAC address.

**Returns:**
- A byte array containing the 6 bytes of the MAC address.

**Example:**
```csharp
var mac = new MacAddress("00:11:22:AA:BB:CC");
byte[] bytes = mac.GetBytes(); // [0x00, 0x11, 0x22, 0xAA, 0xBB, 0xCC]
```

## Supported Input Formats

The `MacAddress` class can parse MAC addresses in any of the following formats:

- Colon-separated (uppercase or lowercase): `00:11:22:AA:BB:CC` or `00:11:22:aa:bb:cc`
- Dash-separated (uppercase or lowercase): `00-11-22-AA-BB-CC` or `00-11-22-aa-bb-cc`
- Dotted (uppercase or lowercase): `0011.22AA.BBCC` or `0011.22aa.bbcc`
- No delimiter (uppercase or lowercase): `001122AABBCC` or `001122aabbcc`

## Complete Example

```csharp
using System;
using Vrelk.Libs.Types.Net;

class Program
{
    static void Main()
    {
        // Create a MAC address from a string
        MacAddress mac = new MacAddress("00:11:22:AA:BB:CC");

        // Display in various formats using properties
        Console.WriteLine($"Colon Uppercase: {mac.ColonUppercase}");
        Console.WriteLine($"Colon Lowercase: {mac.ColonLowercase}");
        Console.WriteLine($"Dash Uppercase: {mac.DashUppercase}");
        Console.WriteLine($"Dash Lowercase: {mac.DashLowercase}");
        Console.WriteLine($"Dotted Uppercase: {mac.DottedUppercase}");
        Console.WriteLine($"Dotted Lowercase: {mac.DottedLowercase}");
        Console.WriteLine($"No Delimiter Uppercase: {mac.NoDelimiterUppercase}");
        Console.WriteLine($"No Delimiter Lowercase: {mac.NoDelimiterLowercase}");

        // Display in a specific format using ToString method
        Console.WriteLine($"Custom Format: {mac.ToString(MacAddress.MacFormat.DashUppercase)}");

        // Check if it's a broadcast or multicast address
        Console.WriteLine($"Is Broadcast: {mac.IsBroadcast()}");
        Console.WriteLine($"Is Multicast: {mac.IsMulticast()}");

        // Get raw bytes
        byte[] bytes = mac.GetBytes();
        Console.WriteLine($"Bytes: {BitConverter.ToString(bytes)}");
    }
}
```

## Output:

```
Colon Uppercase: 00:11:22:AA:BB:CC
Colon Lowercase: 00:11:22:aa:bb:cc
Dash Uppercase: 00-11-22-AA-BB-CC
Dash Lowercase: 00-11-22-aa-bb-cc
Dotted Uppercase: 0011.22AA.BBCC
Dotted Lowercase: 0011.22aa.bbcc
No Delimiter Uppercase: 001122AABBCC
No Delimiter Lowercase: 001122aabbcc
Custom Format: 00-11-22-AA-BB-CC
Is Broadcast: False
Is Multicast: False
Bytes: 00-11-22-AA-BB-CC
```