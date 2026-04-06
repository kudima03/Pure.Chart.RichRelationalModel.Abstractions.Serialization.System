using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

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
        Guid chartId = new Guid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));
        ChartTypeRichRelationalModel type = new ChartTypeRichRelationalModel(
            typeId,
            typeName
        );

        Guid xAxisId = new Guid();
        Guid xAxisChartId = new Guid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));
        AxisRichRelationalModel xAxis = new AxisRichRelationalModel(
            xAxisId,
            xAxisChartId,
            xAxisLegend
        );

        Guid yAxisId = new Guid();
        Guid yAxisChartId = new Guid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));
        AxisRichRelationalModel yAxis = new AxisRichRelationalModel(
            yAxisId,
            yAxisChartId,
            yAxisLegend
        );

        Guid seriesId = new Guid();
        Guid seriesChartId = new Guid();
        IString seriesLegend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));
        SeriesRichRelationalModel series = new SeriesRichRelationalModel(
            seriesId,
            seriesChartId,
            seriesLegend,
            xAxisSource,
            yAxisSource
        );

        IChartRichRelationalModel chart = new ChartRichRelationalModel(
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
        Guid chartId = new Guid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        Guid xAxisId = new Guid();
        Guid xAxisChartId = new Guid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        Guid yAxisId = new Guid();
        Guid yAxisChartId = new Guid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        Guid seriesId = new Guid();
        Guid seriesChartId = new Guid();
        IString seriesLegend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel expected = new ChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new ChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new AxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new AxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            [
                new SeriesRichRelationalModel(
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
        Guid chartId = new Guid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        Guid xAxisId = new Guid();
        Guid xAxisChartId = new Guid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        Guid yAxisId = new Guid();
        Guid yAxisChartId = new Guid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        Guid seriesId = new Guid();
        Guid seriesChartId = new Guid();
        IString seriesLegend = new RandomString(new Char('a'), new Char('z'));
        IString xAxisSource = new RandomString(new Char('a'), new Char('z'));
        IString yAxisSource = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel chart = new ChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new ChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new AxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new AxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            [
                new SeriesRichRelationalModel(
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
        Guid chartId = new Guid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        Guid xAxisId = new Guid();
        Guid xAxisChartId = new Guid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        Guid yAxisId = new Guid();
        Guid yAxisChartId = new Guid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel chart = new ChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new ChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new AxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new AxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
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
        Guid chartId = new Guid();
        IString title = new RandomString(new Char('a'), new Char('z'));
        IString description = new RandomString(new Char('a'), new Char('z'));

        Guid typeId = new Guid();
        IString typeName = new RandomString(new Char('a'), new Char('z'));

        Guid xAxisId = new Guid();
        Guid xAxisChartId = new Guid();
        IString xAxisLegend = new RandomString(new Char('a'), new Char('z'));

        Guid yAxisId = new Guid();
        Guid yAxisChartId = new Guid();
        IString yAxisLegend = new RandomString(new Char('a'), new Char('z'));

        IChartRichRelationalModel chart = new ChartRichRelationalModel(
            chartId,
            title,
            description,
            typeId,
            new ChartTypeRichRelationalModel(typeId, typeName),
            xAxisId,
            new AxisRichRelationalModel(xAxisId, xAxisChartId, xAxisLegend),
            yAxisId,
            new AxisRichRelationalModel(yAxisId, yAxisChartId, yAxisLegend),
            [
                new SeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
                new SeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z')),
                    new RandomString(new Char('a'), new Char('z'))
                ),
                new SeriesRichRelationalModel(
                    new Guid(),
                    new Guid(),
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
