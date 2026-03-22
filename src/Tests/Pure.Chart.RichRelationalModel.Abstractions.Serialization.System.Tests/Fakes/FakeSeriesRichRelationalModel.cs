using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;

internal sealed record FakeSeriesRichRelationalModel : ISeriesRichRelationalModel
{
    public FakeSeriesRichRelationalModel(
        IGuid id,
        IGuid chartId,
        IString legend,
        IString xAxisSource,
        IString yAxisSource
    )
    {
        Id = id;
        ChartId = chartId;
        Legend = legend;
        XAxisSource = xAxisSource;
        YAxisSource = yAxisSource;
    }

    public IGuid Id { get; }

    public IGuid ChartId { get; }

    public IString Legend { get; }

    public IString XAxisSource { get; }

    public IString YAxisSource { get; }
}
