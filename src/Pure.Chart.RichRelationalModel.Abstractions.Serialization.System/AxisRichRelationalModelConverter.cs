using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record AxisRichRelationalModelJsonModel : IAxisRichRelationalModel
{
    public AxisRichRelationalModelJsonModel(IAxisRichRelationalModel model)
        : this(model.Id, model.ChartId, ((IAxisRelationalModel)model).Legend) { }

    [JsonConstructor]
    public AxisRichRelationalModelJsonModel(IGuid id, IGuid chartId, IString legend)
    {
        Id = id;
        ChartId = chartId;
        Legend = legend;
    }

    public IGuid Id { get; }

    public IGuid ChartId { get; }

    public IString Legend { get; }
}

public sealed class AxisRichRelationalModelConverter
    : JsonConverter<IAxisRichRelationalModel>
{
    public override IAxisRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<AxisRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IAxisRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new AxisRichRelationalModelJsonModel(value),
            options
        );
    }
}
