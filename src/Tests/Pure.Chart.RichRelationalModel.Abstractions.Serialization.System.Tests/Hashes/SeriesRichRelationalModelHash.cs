using System.Collections;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Hashes;

internal sealed record SeriesRichRelationalModelHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        53,
        112,
        151,
        1,
        204,
        78,
        33,
        95,
        187,
        61,
        149,
        230,
        63,
        17,
        88,
        241,
    ];

    private readonly ISeriesRichRelationalModel _model;

    public SeriesRichRelationalModelHash(ISeriesRichRelationalModel model)
    {
        _model = model;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix
                .Concat(new DeterminedHash(_model.Id))
                .Concat(new DeterminedHash(_model.ChartId))
                .Concat(new DeterminedHash(((ISeriesRelationalModel)_model).Legend))
                .Concat(new DeterminedHash(((ISeriesRelationalModel)_model).XAxisSource))
                .Concat(new DeterminedHash(((ISeriesRelationalModel)_model).YAxisSource))
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
