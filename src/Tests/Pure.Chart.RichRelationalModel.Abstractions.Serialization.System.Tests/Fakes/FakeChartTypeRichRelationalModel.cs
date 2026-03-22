using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;

internal sealed record FakeChartTypeRichRelationalModel : IChartTypeRichRelationalModel
{
    public FakeChartTypeRichRelationalModel(IGuid id, IString name)
    {
        Id = id;
        Name = name;
    }

    public IGuid Id { get; }

    public IString Name { get; }
}
