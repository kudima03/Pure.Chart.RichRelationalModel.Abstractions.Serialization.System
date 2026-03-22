using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;
using Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Hashes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record SeriesRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public SeriesRichRelationalModelConverterTests()
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
        FakeGuid id = new FakeGuid();
        FakeGuid chartId = new FakeGuid();
        IString legend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        ISeriesRichRelationalModel series = new FakeSeriesRichRelationalModel(
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
        FakeGuid id = new FakeGuid();
        FakeGuid chartId = new FakeGuid();
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
            new SeriesRichRelationalModelHash(
                new FakeSeriesRichRelationalModel(
                    id,
                    chartId,
                    legend,
                    xAxisSource,
                    yAxisSource
                )
            ).SequenceEqual(
                new SeriesRichRelationalModelHash(
                    JsonSerializer.Deserialize<ISeriesRichRelationalModel>(
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
        FakeGuid id = new FakeGuid();
        FakeGuid chartId = new FakeGuid();
        IString legend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        ISeriesRichRelationalModel series = new FakeSeriesRichRelationalModel(
            id,
            chartId,
            legend,
            xAxisSource,
            yAxisSource
        );

        ISeriesRichRelationalModel deserialized =
            JsonSerializer.Deserialize<ISeriesRichRelationalModel>(
                JsonSerializer.Serialize(series, _options),
                _options
            )!;

        Assert.True(
            new SeriesRichRelationalModelHash(series).SequenceEqual(
                new SeriesRichRelationalModelHash(deserialized)
            )
        );
    }
}
