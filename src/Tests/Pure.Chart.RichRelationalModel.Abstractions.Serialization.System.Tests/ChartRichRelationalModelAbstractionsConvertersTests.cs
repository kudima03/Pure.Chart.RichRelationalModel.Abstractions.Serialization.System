using System.Collections;
using System.Text.Json.Serialization;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record ChartRichRelationalModelAbstractionsConvertersTests
{
    [Fact]
    public void EnumeratesFourConverters()
    {
        Assert.Equal(4, new ChartRichRelationalModelAbstractionsConverters().Count());
    }

    [Fact]
    public void EnumeratesCorrectConverterTypes()
    {
        IEnumerable<JsonConverter> converters =
            new ChartRichRelationalModelAbstractionsConverters();

        _ = Assert.IsType<AxisRichRelationalModelConverter>(converters.ElementAt(0));
        _ = Assert.IsType<ChartTypeRichRelationalModelConverter>(converters.ElementAt(1));
        _ = Assert.IsType<SeriesRichRelationalModelConverter>(converters.ElementAt(2));
        _ = Assert.IsType<ChartRichRelationalModelConverter>(converters.ElementAt(3));
    }

    [Fact]
    public void NonGenericEnumeratorReturnsAllConverters()
    {
        IEnumerable converters = new ChartRichRelationalModelAbstractionsConverters();

        int count = 0;

        foreach (object _ in converters)
        {
            count++;
        }

        Assert.Equal(4, count);
    }
}
