using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.Chart.RichRelationalModel.HashCodes;
using Pure.Primitives.Abstractions.Serialization.System;
using Pure.Primitives.Abstractions.String;
using Pure.Primitives.Random.String;
using Char = Pure.Primitives.Char.Char;
using Guid = Pure.Primitives.Guid.Guid;

namespace Pure.Chart.RichRelationalModel.Abstractions.Serialization.System.Tests;

public sealed record AxisRichRelationalModelConverterTests
{
    private readonly JsonSerializerOptions _options;

    public AxisRichRelationalModelConverterTests()
    {
        _options = new JsonSerializerOptions();

        foreach (JsonConverter converter in new PrimitiveConverters())
        {
            _options.Converters.Add(converter);
        }

        foreach (
            JsonConverter converter in new ChartRichRelationalModelAbstractionsConverters()
        )
        {
            _options.Converters.Add(converter);
        }

        _options.WriteIndented = true;
        _options.NewLine = "\n";
    }

    [Fact]
    public void Write()
    {
        Guid id = new Guid();
        IString legend = new RandomString(new Char('a'), new Char('z'));

        IAxisRichRelationalModel axis = new AxisRichRelationalModel(id, legend);

        string serialized = JsonSerializer.Serialize(axis, _options);

        Assert.Equal(
            $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Legend": "{{legend.TextValue}}"
            }
            """,
            serialized
        );
    }

    [Fact]
    public void Read()
    {
        Guid id = new Guid();
        IString legend = new RandomString(new Char('a'), new Char('z'));

        string input = $$"""
            {
              "Id": "{{id.GuidValue}}",
              "Legend": "{{legend.TextValue}}"
            }
            """;

        Assert.True(
            new AxisRichRelationalModelHash(
                new AxisRichRelationalModel(id, legend)
            ).SequenceEqual(
                new AxisRichRelationalModelHash(
                    JsonSerializer.Deserialize<IAxisRichRelationalModel>(input, _options)!
                )
            )
        );
    }

    [Fact]
    public void RoundTrip()
    {
        Guid id = new Guid();
        IString legend = new RandomString(new Char('a'), new Char('z'));

        IAxisRichRelationalModel axis = new AxisRichRelationalModel(id, legend);

        IAxisRichRelationalModel deserialized =
            JsonSerializer.Deserialize<IAxisRichRelationalModel>(
                JsonSerializer.Serialize(axis, _options),
                _options
            )!;

        Assert.True(
            new AxisRichRelationalModelHash(axis).SequenceEqual(
                new AxisRichRelationalModelHash(deserialized)
            )
        );
    }
}
