using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;

internal sealed record FakeAxisRichRelationalModel : IAxisRichRelationalModel
{
    public FakeAxisRichRelationalModel(IGuid id, IGuid chartId, IString legend)
    {
        Id = id;
        ChartId = chartId;
        Legend = legend;
    }

    public IGuid Id { get; }

    public IGuid ChartId { get; }

    public IString Legend { get; }
}
