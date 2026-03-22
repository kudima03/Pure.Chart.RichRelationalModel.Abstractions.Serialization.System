using System.Collections;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Hashes;

internal sealed record AxisRichRelationalModelHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        17,
        42,
        151,
        1,
        83,
        201,
        64,
        119,
        142,
        37,
        198,
        12,
        74,
        163,
        55,
        91,
    ];

    private readonly IAxisRichRelationalModel _model;

    public AxisRichRelationalModelHash(IAxisRichRelationalModel model)
    {
        _model = model;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix
                .Concat(new DeterminedHash(_model.Id))
                .Concat(new DeterminedHash(_model.ChartId))
                .Concat(new DeterminedHash(((IAxisRelationalModel)_model).Legend))
        ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
