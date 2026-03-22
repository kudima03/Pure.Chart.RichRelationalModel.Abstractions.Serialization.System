using Pure.Primitives.Abstractions.Guid;
using SystemGuid = System.Guid;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Fakes;

internal sealed record FakeGuid : IGuid
{
    public FakeGuid()
        : this(SystemGuid.NewGuid()) { }

    public FakeGuid(SystemGuid value)
    {
        GuidValue = value;
    }

    public SystemGuid GuidValue { get; }
}
