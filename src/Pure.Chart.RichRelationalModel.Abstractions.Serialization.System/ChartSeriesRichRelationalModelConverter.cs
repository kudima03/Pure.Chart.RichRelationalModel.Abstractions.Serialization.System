using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record ChartSeriesRichRelationalModelJsonModel
    : IChartSeriesRichRelationalModel
{
    public ChartSeriesRichRelationalModelJsonModel(IChartSeriesRichRelationalModel model)
        : this(
            model.Id,
            model.ChartId,
            ((IChartSeriesRelationalModel)model).Legend,
            ((IChartSeriesRelationalModel)model).XAxisSource,
            ((IChartSeriesRelationalModel)model).YAxisSource
        )
    { }

    [JsonConstructor]
    public ChartSeriesRichRelationalModelJsonModel(
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

public sealed class ChartSeriesRichRelationalModelConverter
    : JsonConverter<IChartSeriesRichRelationalModel>
{
    public override IChartSeriesRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ChartSeriesRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IChartSeriesRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ChartSeriesRichRelationalModelJsonModel(value),
            options
        );
    }
}
