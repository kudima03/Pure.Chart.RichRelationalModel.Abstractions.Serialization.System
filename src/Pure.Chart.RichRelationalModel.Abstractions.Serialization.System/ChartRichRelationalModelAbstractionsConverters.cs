using System.Collections;
using System.Text.Json.Serialization;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

public sealed class ChartRichRelationalModelAbstractionsConverters
    : IEnumerable<JsonConverter>
{
    public IEnumerator<JsonConverter> GetEnumerator()
    {
        yield return new AxisRichRelationalModelConverter();
        yield return new ChartTypeRichRelationalModelConverter();
        yield return new SeriesRichRelationalModelConverter();
        yield return new ChartRichRelationalModelConverter();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
