# Taco Parser

A C# application that finds the two Taco Bell locations that are farthest apart from each other using geolocation calculations, CSV parsing, and structured logging.

## Overview

This project parses a CSV file containing Taco Bell locations (latitude, longitude, and name) and calculates the distance between all location pairs to determine which two locations are the farthest apart. The application uses 3D vector space mathematics for distance calculations and implements structured logging with ILogger and Serilog.

## Features

### 3D Vector Space Distance Calculations
- **Vector3D Class**: Custom implementation for 3D coordinate calculations
- Converts latitude/longitude to 3D Cartesian coordinates (x, y, z) on Earth's surface
- Calculates great circle distance (arc distance along Earth's surface) using vector dot product and angle calculations
- Educational implementation that demonstrates the mathematics behind geolocation distance calculations
- Note: GeoCoordinate.NetCore provides the same functionality with less code, but this implementation shows the underlying math

### ILogger Integration with Serilog
- **ILogger Interface**: Uses Microsoft.Extensions.Logging.ILogger<T> for standard .NET logging
- **Serilog Provider**: Serilog configured as the logging provider, implementing ILogger
- **Benefits**: 
  - Standard .NET logging pattern (dependency injection friendly)
  - Serilog's powerful features (file logging, structured logging, console output)
  - Better testability (can mock ILogger in unit tests)
  - Flexible (can swap logging providers without changing code)

## Packages Used
- `Serilog` - Structured logging framework
- `Serilog.Sinks.Console` - Console output
- `Serilog.Sinks.File` - File logging with rolling intervals
- `Serilog.Extensions.Logging` - Integration with Microsoft.Extensions.Logging
- `Microsoft.Extensions.Logging` - Standard .NET logging interface
- `GeoCoordinate.NetCore` - (Original implementation, now commented out for reference)

## Technical Details

### 3D Vector Space Implementation

The `Vector3D` class converts spherical coordinates (latitude, longitude) to 3D Cartesian coordinates:

```csharp
// Conversion formula (Earth's radius = 6,371,000 meters)
x = R * cos(lat) * cos(long)
y = R * cos(lat) * sin(long)
z = R * sin(lat)
```

Distance is calculated using great circle distance (arc distance along Earth's surface):

```csharp
// Calculate dot product of the two vectors
dotProduct = vec1 · vec2

// Calculate angle between vectors
cos(angle) = dotProduct / (R × R)
angle = arccos(cos(angle))

// Great circle distance = angle × Earth's radius
distance = angle × R
```

**Distance Calculation**: 

The current implementation calculates **great circle distance** (arc distance along Earth's curved surface), which follows the Earth's topography/curvature. This is the same type of distance that GeoCoordinate's `GetDistanceTo()` method calculates.

- **Great Circle Distance (Current 3D Vector)**: Arc distance along Earth's surface following curvature
- **Great Circle Distance (GeoCoordinate)**: Same calculation, but using built-in library methods

**Why use 3D vectors instead of GeoCoordinate?**
- **Educational value**: Demonstrates the underlying mathematics (dot product, angle calculation, spherical geometry)
- **Understanding**: Shows how 3D coordinates can be used for geolocation calculations
- **Customization**: Easier to modify or extend for specific needs
- **No dependency**: One less NuGet package (though GeoCoordinate is lightweight)

**When to use GeoCoordinate instead:**
- Simpler code with less maintenance
- Well-tested library with edge cases handled
- Production applications where simplicity > educational value

### Logging Setup

The `Startup.InitializeLogger()` method:
1. Configures Serilog with console and file sinks
2. Creates an ILoggerFactory with Serilog as the provider
3. Returns the factory for creating ILogger<T> instances

Example usage:
```csharp
var loggerFactory = Startup.InitializeLogger();
var logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Application Started");
```

All logging goes through the ILogger interface but is powered by Serilog underneath, providing both standard .NET patterns and Serilog's advanced features.

## How It Works

1. **CSV Parsing**: Reads Taco Bell location data from a CSV file
2. **Location Conversion**: Converts each location's latitude/longitude to 3D Cartesian coordinates
3. **Distance Calculation**: Compares all location pairs using great circle distance calculations (arc distance along Earth's surface)
4. **Result**: Identifies and displays the two locations that are farthest apart, along with the distance in miles

## Project Structure

- `LoggingKata/Program.cs` - Main application entry point
- `LoggingKata/TacoParser.cs` - CSV parsing logic
- `LoggingKata/Vector3D.cs` - 3D vector space calculations
- `LoggingKata/Startup.cs` - Logger configuration
- `LoggingKata/ConvertDistance.cs` - Distance unit conversion utilities
