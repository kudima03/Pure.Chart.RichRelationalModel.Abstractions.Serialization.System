using System.Collections;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Hashes;

internal sealed record ChartTypeRichRelationalModelHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        29,
        88,
        151,
        1,
        117,
        34,
        91,
        107,
        163,
        52,
        211,
        87,
        31,
        199,
        142,
        73,
    ];

    private readonly IChartTypeRichRelationalModel _model;

    public ChartTypeRichRelationalModelHash(IChartTypeRichRelationalModel model)
    {
        _model = model;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix
                .Concat(new DeterminedHash(_model.Id))
                .Concat(new DeterminedHash(((IChartTypeRelationalModel)_model).Name))
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
