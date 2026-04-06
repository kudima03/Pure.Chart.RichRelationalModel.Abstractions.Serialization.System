using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.Model.Abstractions;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record ChartRichRelationalModelJsonModel : IChartRichRelationalModel
{
    public ChartRichRelationalModelJsonModel(IChartRichRelationalModel model)
        : this(
            model.Id,
            ((IChartRelationalModel)model).Title,
            ((IChartRelationalModel)model).Description,
            model.TypeId,
            (IChartTypeRichRelationalModel)model.Type,
            model.XAxisId,
            (IAxisRichRelationalModel)model.XAxis,
            model.YAxisId,
            (IAxisRichRelationalModel)model.YAxis,
            model.Series.Cast<ISeriesRichRelationalModel>()
        )
    { }

    [JsonConstructor]
    public ChartRichRelationalModelJsonModel(
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

public sealed class ChartRichRelationalModelConverter
    : JsonConverter<IChartRichRelationalModel>
{
    public override IChartRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ChartRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IChartRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ChartRichRelationalModelJsonModel(value),
            options
        );
    }
}
