using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RelationalModel.Abstractions;
using Pure.Primitives.Abstractions.Guid;
using Pure.Primitives.Abstractions.String;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System;

internal sealed record ChartTypeRichRelationalModelJsonModel
    : IChartTypeRichRelationalModel
{
    public ChartTypeRichRelationalModelJsonModel(IChartTypeRichRelationalModel model)
        : this(model.Id, ((IChartTypeRelationalModel)model).Name) { }

    [JsonConstructor]
    public ChartTypeRichRelationalModelJsonModel(IGuid id, IString name)
    {
        Id = id;
        Name = name;
    }

    public IGuid Id { get; }

    public IString Name { get; }
}

public sealed class ChartTypeRichRelationalModelConverter
    : JsonConverter<IChartTypeRichRelationalModel>
{
    public override IChartTypeRichRelationalModel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ChartTypeRichRelationalModelJsonModel>(
            ref reader,
            options
        )!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IChartTypeRichRelationalModel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ChartTypeRichRelationalModelJsonModel(value),
            options
        );
    }
}
