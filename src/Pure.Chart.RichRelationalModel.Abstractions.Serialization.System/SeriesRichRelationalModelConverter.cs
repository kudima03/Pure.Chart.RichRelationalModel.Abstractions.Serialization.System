using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record SeriesRichRelationalModelJsonModel : ISeriesRichRelationalModel
{
    public SeriesRichRelationalModelJsonModel(ISeriesRichRelationalModel model)
        : this(
            model.Id,
            model.ChartId,
            ((ISeriesRelationalModel)model).Legend,
            ((ISeriesRelationalModel)model).XAxisSource,
            ((ISeriesRelationalModel)model).YAxisSource
        )
    { }

    [JsonConstructor]
    public SeriesRichRelationalModelJsonModel(
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

public sealed class SeriesRichRelationalModelConverter
    : JsonConverter<ISeriesRichRelationalModel>
{
    public override ISeriesRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<SeriesRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ISeriesRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new SeriesRichRelationalModelJsonModel(value),
            options
        );
    }
}
