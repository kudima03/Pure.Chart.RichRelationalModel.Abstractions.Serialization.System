using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartSeriesRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public ChartSeriesRichRelationalModelConverterTests()
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
        Guid chartId = new Guid();
        IString legend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartSeriesRichRelationalModel series = new ChartSeriesRichRelationalModel(
            id,
            chartId,
            legend,
            xAxisSource,
            yAxisSource
        );

        string serialized = JsonSerializer.Serialize(series, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "ChartId": "{{chartId.GuidValue}}",
              "Legend": "{{legend.TextValue}}",
              "XAxisSource": "{{xAxisSource.TextValue}}",
              "YAxisSource": "{{yAxisSource.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        Guid chartId = new Guid();
        IString legend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "ChartId": "{{chartId.GuidValue}}",
              "Legend": "{{legend.TextValue}}",
              "XAxisSource": "{{xAxisSource.TextValue}}",
              "YAxisSource": "{{yAxisSource.TextValue}}"
            }
            """;

        Assert.True(
            new ChartSeriesRichRelationalModelHash(
                new ChartSeriesRichRelationalModel(
                    id,
                    chartId,
                    legend,
                    xAxisSource,
                    yAxisSource
                )
            ).SequenceEqual(
                new ChartSeriesRichRelationalModelHash(
                    JsonSerializer.Deserialize<IChartSeriesRichRelationalModel>(
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
        Guid chartId = new Guid();
        IString legend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartSeriesRichRelationalModel series = new ChartSeriesRichRelationalModel(
            id,
            chartId,
            legend,
            xAxisSource,
            yAxisSource
        );

        IChartSeriesRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartSeriesRichRelationalModel>(
                JsonSerializer.Serialize(series, _options),
                _options
            )!;

        Assert.True(
            new ChartSeriesRichRelationalModelHash(series).SequenceEqual(
                new ChartSeriesRichRelationalModelHash(deserialized)
            )
        );
    }
}
