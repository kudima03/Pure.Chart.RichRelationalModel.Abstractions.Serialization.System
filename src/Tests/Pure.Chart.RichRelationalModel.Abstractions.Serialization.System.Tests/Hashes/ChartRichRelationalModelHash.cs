using System.Collections;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.HashCodes;
using Pure.HashCodes.Abstractions;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests.Hashes;

internal sealed record ChartRichRelationalModelHash : IDeterminedHash
{
    private static readonly byte[] TypePrefix =
    [
        71,
        134,
        151,
        1,
        39,
        167,
        52,
        83,
        219,
        94,
        177,
        13,
        108,
        245,
        61,
        182,
    ];

    private readonly IChartRichRelationalModel _model;

    public ChartRichRelationalModelHash(IChartRichRelationalModel model)
    {
        _model = model;
    }

    public IEnumerator<byte> GetEnumerator()
    {
        return new DeterminedHash(
            TypePrefix
                .Concat(new DeterminedHash(_model.Id))
                .Concat(new DeterminedHash(((IChartRelationalModel)_model).Title))
                .Concat(new DeterminedHash(((IChartRelationalModel)_model).Description))
                .Concat(new DeterminedHash(_model.TypeId))
                .Concat(
                    new ChartTypeRichRelationalModelHash(
                        (IChartTypeRichRelationalModel)_model.Type
                    )
                )
                .Concat(new DeterminedHash(_model.XAxisId))
                .Concat(
                    new AxisRichRelationalModelHash(
                        (IAxisRichRelationalModel)_model.XAxis
                    )
                )
                .Concat(new DeterminedHash(_model.YAxisId))
                .Concat(
                    new AxisRichRelationalModelHash(
                        (IAxisRichRelationalModel)_model.YAxis
                    )
                )
                .Concat(
                    new DeterminedHash(
                        _model
                            .Series.Cast<ISeriesRichRelationalModel>()
                            .Select(s =>
                                (IDeterminedHash)new SeriesRichRelationalModelHash(s)
                            )
                    )
                )
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
