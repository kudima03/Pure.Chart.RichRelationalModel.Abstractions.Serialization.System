using Pure.Chart.Model.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;

internal sealed record FakeChartRichRelationalModel : IChartRichRelationalModel
{
    public FakeChartRichRelationalModel(
        IGuid id,
        IString title,
        IString description,
        IGuid typeId,
        IChartTypeRichRelationalModel type,
        IGuid xAxisId,
        IAxisRichRelationalModel xAxis,
        IGuid yAxisId,
        IAxisRichRelationalModel yAxis,
        IEnumerable<ISeriesRichRelationalModel> series
    )
    {
        Id = id;
        Title = title;
        Description = description;
        TypeId = typeId;
        Type = type;
        XAxisId = xAxisId;
        XAxis = xAxis;
        YAxisId = yAxisId;
        YAxis = yAxis;
        Series = series;
    }

    public IGuid Id { get; }

    public IString Title { get; }

    public IString Description { get; }

    public IGuid TypeId { get; }

    public IChartTypeRichRelationalModel Type { get; }

    public IGuid XAxisId { get; }

    public IAxisRichRelationalModel XAxis { get; }

    public IGuid YAxisId { get; }

    public IAxisRichRelationalModel YAxis { get; }

    public IEnumerable<ISeriesRichRelationalModel> Series { get; }

    IChartType IChart.Type => Type;

    IAxis IChart.XAxis => XAxis;

    IAxis IChart.YAxis => YAxis;

    IEnumerable<ISeries> IChart.Series => Series;
}
