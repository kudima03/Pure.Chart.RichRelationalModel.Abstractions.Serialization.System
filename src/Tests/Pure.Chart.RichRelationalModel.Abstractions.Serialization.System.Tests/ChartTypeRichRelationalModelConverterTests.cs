using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartTypeRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public ChartTypeRichRelationalModelConverterTests()
    {
        _options = new JsonSerializerOptions();

        foreach (JsonConverter converter in new PrimitiveConverters())
        {
            _options.Converters.Add(converter);
        }

        foreach (
            JsonConverter converter in new ChartRichRelationalModelAbstractionsConverters()
        )
        {
            _options.Converters.Add(converter);
        }

        _options.WriteIndented = true;
        _options.NewLine = "\n";
    }

    [Fact]
    public void Write()
    {
        Guid id = new Guid();
        IString name = new RandomString(new Char('a'), new Char('z'));

        IChartTypeRichRelationalModel chartType = new ChartTypeRichRelationalModel(
            id,
            name
        );

        string serialized = JsonSerializer.Serialize(chartType, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Name": "{{name.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        IString name = new RandomString(new Char('a'), new Char('z'));

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Name": "{{name.TextValue}}"
            }
            """;

        Assert.True(
            new ChartTypeRichRelationalModelHash(
                new ChartTypeRichRelationalModel(id, name)
            ).SequenceEqual(
                new ChartTypeRichRelationalModelHash(
                    JsonSerializer.Deserialize<IChartTypeRichRelationalModel>(
                        input,
                        _options
                    )!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        Guid id = new Guid();
        IString name = new RandomString(new Char('a'), new Char('z'));

        IChartTypeRichRelationalModel chartType = new ChartTypeRichRelationalModel(
            id,
            name
        );

        IChartTypeRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartTypeRichRelationalModel>(
                JsonSerializer.Serialize(chartType, _options),
                _options
            )!;

        Assert.True(
            new ChartTypeRichRelationalModelHash(chartType).SequenceEqual(
                new ChartTypeRichRelationalModelHash(deserialized)
            )
        );
    }
}
