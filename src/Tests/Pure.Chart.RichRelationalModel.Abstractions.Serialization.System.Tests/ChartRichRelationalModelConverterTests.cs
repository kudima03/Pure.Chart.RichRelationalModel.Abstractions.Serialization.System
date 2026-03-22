using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;
using Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Hashes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public ChartRichRelationalModelConverterTests()
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
        FakeGuid chartId = new FakeGuid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        FakeGuid typeId = new FakeGuid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));
        FakeChartTypeRichRelationalModel type = new FakeChartTypeRichRelationalModel(
            typeId,
            typeName
        );

        FakeGuid xAxisId = new FakeGuid();
        FakeGuid xAxisChartId = new FakeGuid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));
        FakeAxisRichRelationalModel xAxis = new FakeAxisRichRelationalModel(
            xAxisId,
            xAxisChartId,
            xAxisLegend
        );

        FakeGuid yAxisId = new FakeGuid();
        FakeGuid yAxisChartId = new FakeGuid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));
        FakeAxisRichRelationalModel yAxis = new FakeAxisRichRelationalModel(
            yAxisId,
            yAxisChartId,
            yAxisLegend
        );

        FakeGuid seriesId = new FakeGuid();
        FakeGuid seriesChartId = new FakeGuid();
        IString seriesLegend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));
        FakeSeriesRichRelationalModel series = new FakeSeriesRichRelationalModel(
            seriesId,
            seriesChartId,
            seriesLegend,
            xAxisSource,
            yAxisSource
        );

        IChartRichRelationalModel chart = new FakeChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            type,
            xAxisId,
            xAxis,
            yAxisId,
            yAxis,
            [series]
        );

        string serialized = JsonSerializer.Serialize(chart, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{chartId.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "Type": {
                "Id": "{{typeId.GuidValue}}",
                "Name": "{{typeName.TextValue}}"
              },
              "XAxisId": "{{xAxisId.GuidValue}}",
              "XAxis": {
                "Id": "{{xAxisId.GuidValue}}",
                "ChartId": "{{xAxisChartId.GuidValue}}",
                "Legend": "{{xAxisLegend.TextValue}}"
              },
              "YAxisId": "{{yAxisId.GuidValue}}",
              "YAxis": {
                "Id": "{{yAxisId.GuidValue}}",
                "ChartId": "{{yAxisChartId.GuidValue}}",
                "Legend": "{{yAxisLegend.TextValue}}"
              },
              "Series": [
                {
                  "Id": "{{seriesId.GuidValue}}",
                  "ChartId": "{{seriesChartId.GuidValue}}",
                  "Legend": "{{seriesLegend.TextValue}}",
                  "XAxisSource": "{{xAxisSource.TextValue}}",
                  "YAxisSource": "{{yAxisSource.TextValue}}"
                }
              ]
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        FakeGuid chartId = new FakeGuid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        FakeGuid typeId = new FakeGuid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        FakeGuid xAxisId = new FakeGuid();
        FakeGuid xAxisChartId = new FakeGuid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        FakeGuid yAxisId = new FakeGuid();
        FakeGuid yAxisChartId = new FakeGuid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        FakeGuid seriesId = new FakeGuid();
        FakeGuid seriesChartId = new FakeGuid();
        IString seriesLegend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel expected = new FakeChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new FakeChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new FakeAxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new FakeAxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            [
                new FakeSeriesRichRelationalModel(
                    seriesId,
                    seriesChartId,
                    seriesLegend,
                    xAxisSource,
                    yAxisSource
                ),
            ]
        );

        string input = $$"""
            {
              "Id": "{{chartId.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "Type": {
                "Id": "{{typeId.GuidValue}}",
                "Name": "{{typeName.TextValue}}"
              },
              "XAxisId": "{{xAxisId.GuidValue}}",
              "XAxis": {
                "Id": "{{xAxisId.GuidValue}}",
                "ChartId": "{{xAxisChartId.GuidValue}}",
                "Legend": "{{xAxisLegend.TextValue}}"
              },
              "YAxisId": "{{yAxisId.GuidValue}}",
              "YAxis": {
                "Id": "{{yAxisId.GuidValue}}",
                "ChartId": "{{yAxisChartId.GuidValue}}",
                "Legend": "{{yAxisLegend.TextValue}}"
              },
              "Series": [
                {
                  "Id": "{{seriesId.GuidValue}}",
                  "ChartId": "{{seriesChartId.GuidValue}}",
                  "Legend": "{{seriesLegend.TextValue}}",
                  "XAxisSource": "{{xAxisSource.TextValue}}",
                  "YAxisSource": "{{yAxisSource.TextValue}}"
                }
              ]
            }
            """;

        Assert.True(
            new ChartRichRelationalModelHash(expected).SequenceEqual(
                new ChartRichRelationalModelHash(
                    JsonSerializer.Deserialize<IChartRichRelationalModel>(
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
        FakeGuid chartId = new FakeGuid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        FakeGuid typeId = new FakeGuid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        FakeGuid xAxisId = new FakeGuid();
        FakeGuid xAxisChartId = new FakeGuid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        FakeGuid yAxisId = new FakeGuid();
        FakeGuid yAxisChartId = new FakeGuid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        FakeGuid seriesId = new FakeGuid();
        FakeGuid seriesChartId = new FakeGuid();
        IString seriesLegend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel chart = new FakeChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new FakeChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new FakeAxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new FakeAxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            [
                new FakeSeriesRichRelationalModel(
                    seriesId,
                    seriesChartId,
                    seriesLegend,
                    xAxisSource,
                    yAxisSource
                ),
            ]
        );

        IChartRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartRichRelationalModel>(
                JsonSerializer.Serialize(chart, _options),
                _options
            )!;

        Assert.True(
            new ChartRichRelationalModelHash(chart).SequenceEqual(
                new ChartRichRelationalModelHash(deserialized)
            )
        );
    }

    [Fact]
    public void WriteNoSeries()
    {
        FakeGuid chartId = new FakeGuid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        FakeGuid typeId = new FakeGuid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        FakeGuid xAxisId = new FakeGuid();
        FakeGuid xAxisChartId = new FakeGuid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        FakeGuid yAxisId = new FakeGuid();
        FakeGuid yAxisChartId = new FakeGuid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel chart = new FakeChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new FakeChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new FakeAxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new FakeAxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            []
        );

        string serialized = JsonSerializer.Serialize(chart, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{chartId.GuidValue}}",
              "Title": "{{title.TextValue}}",
              "Description": "{{description.TextValue}}",
              "TypeId": "{{typeId.GuidValue}}",
              "Type": {
                "Id": "{{typeId.GuidValue}}",
                "Name": "{{typeName.TextValue}}"
              },
              "XAxisId": "{{xAxisId.GuidValue}}",
              "XAxis": {
                "Id": "{{xAxisId.GuidValue}}",
                "ChartId": "{{xAxisChartId.GuidValue}}",
                "Legend": "{{xAxisLegend.TextValue}}"
              },
              "YAxisId": "{{yAxisId.GuidValue}}",
              "YAxis": {
                "Id": "{{yAxisId.GuidValue}}",
                "ChartId": "{{yAxisChartId.GuidValue}}",
                "Legend": "{{yAxisLegend.TextValue}}"
              },
              "Series": []
            }
            """,
            serialized
        );
    }

    [Fact]
    public void RoundTripMultipleSeries()
    {
        FakeGuid chartId = new FakeGuid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        FakeGuid typeId = new FakeGuid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        FakeGuid xAxisId = new FakeGuid();
        FakeGuid xAxisChartId = new FakeGuid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        FakeGuid yAxisId = new FakeGuid();
        FakeGuid yAxisChartId = new FakeGuid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel chart = new FakeChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new FakeChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new FakeAxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new FakeAxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            [
                new FakeSeriesRichRelationalModel(
                    new FakeGuid(),
                    new FakeGuid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
                new FakeSeriesRichRelationalModel(
                    new FakeGuid(),
                    new FakeGuid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
                new FakeSeriesRichRelationalModel(
                    new FakeGuid(),
                    new FakeGuid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
            ]
        );

        IChartRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IChartRichRelationalModel>(
                JsonSerializer.Serialize(chart, _options),
                _options
            )!;

        Assert.True(
            new ChartRichRelationalModelHash(chart).SequenceEqual(
                new ChartRichRelationalModelHash(deserialized)
            )
        );
    }
}
