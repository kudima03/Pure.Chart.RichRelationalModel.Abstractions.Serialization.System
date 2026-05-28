# Pure.Chart.RichRelationalModel.Abstractions.Serialization.System

`System.Text.Json` converters for the **Pure.Chart** rich relational model abstractions.

[![.NET build & test](https://github.com/kudima03/Pure.Chart.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Chart.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Chart.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Chart.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Chart.RichRelationalModel.Abstractions.Serialization.System)](https://www.nuget.org/packages/Pure.Chart.RichRelationalModel.Abstractions.Serialization.System)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.txt)

## Overview

`Pure.Chart.RichRelationalModel.Abstractions.Serialization.System` provides `System.Text.Json` converters that enable serialization and deserialization of the `IChart*RichRelationalModel` interfaces defined in [`Pure.Chart.RichRelationalModel.Abstractions`](https://github.com/kudima03/Pure.Chart.RichRelationalModel.Abstractions). Each converter maps between JSON and the corresponding interface by using an internal record type that implements the interface.

## Converters

| Type | Converts |
|---|---|
| `ChartRichRelationalModelConverter` | `IChartRichRelationalModel` |
| `ChartTypeRichRelationalModelConverter` | `IChartTypeRichRelationalModel` |
| `AxisRichRelationalModelConverter` | `IAxisRichRelationalModel` |
| `ChartSeriesRichRelationalModelConverter` | `IChartSeriesRichRelationalModel` |
| `ChartRichRelationalModelAbstractionsConverters` | `IEnumerable<JsonConverter>` containing all four converters above |

`ChartRichRelationalModelAbstractionsConverters` is the entry point — enumerate it to register all converters at once.

## Dependencies

- [`Pure.Chart.RichRelationalModel.Abstractions`](https://github.com/kudima03/Pure.Chart.RichRelationalModel.Abstractions/tree/0.1.0-preview.4.0.0) — composite `IChart*RichRelationalModel` interfaces that combine the base chart model interfaces with their relational model counterparts

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```
dotnet add package Pure.Chart.RichRelationalModel.Abstractions.Serialization.System
```

## Usage

```csharp
using System.Text.Json;
using Pure.Chart.RichRelationalModel.Abstractions;
using Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

JsonSerializerOptions options = new JsonSerializerOptions();

foreach (JsonConverter converter in new ChartRichRelationalModelAbstractionsConverters())
{
    options.Converters.Add(converter);
}

string json = JsonSerializer.Serialize(chart, options);
IChartRichRelationalModel deserialized =
    JsonSerializer.Deserialize<IChartRichRelationalModel>(json, options)!;
```
