using System.Globalization;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.CommandsNext.Converters
{
    public class BoolConverter : IArgumentConverter<bool>
    {
        Task<Optional<bool>> IArgumentConverter<bool>.ConvertAsync(string value, CommandContext ctx)
        {
            return bool.TryParse(value, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<bool>());
        }
    }

    public class Int8Converter : IArgumentConverter<sbyte>
    {
        Task<Optional<sbyte>> IArgumentConverter<sbyte>.ConvertAsync(string value, CommandContext ctx)
        {
            return sbyte.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<sbyte>());
        }
    }

    public class Uint8Converter : IArgumentConverter<byte>
    {
        Task<Optional<byte>> IArgumentConverter<byte>.ConvertAsync(string value, CommandContext ctx)
        {
            return byte.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<byte>());
        }
    }

    public class Int16Converter : IArgumentConverter<short>
    {
        Task<Optional<short>> IArgumentConverter<short>.ConvertAsync(string value, CommandContext ctx)
        {
            return short.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<short>());
        }
    }

    public class Uint16Converter : IArgumentConverter<ushort>
    {
        Task<Optional<ushort>> IArgumentConverter<ushort>.ConvertAsync(string value, CommandContext ctx)
        {
            return ushort.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<ushort>());
        }
    }

    public class Int32Converter : IArgumentConverter<int>
    {
        Task<Optional<int>> IArgumentConverter<int>.ConvertAsync(string value, CommandContext ctx)
        {
            return int.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<int>());
        }
    }

    public class Uint32Converter : IArgumentConverter<uint>
    {
        Task<Optional<uint>> IArgumentConverter<uint>.ConvertAsync(string value, CommandContext ctx)
        {
            return uint.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<uint>());
        }
    }

    public class Int64Converter : IArgumentConverter<long>
    {
        Task<Optional<long>> IArgumentConverter<long>.ConvertAsync(string value, CommandContext ctx)
        {
            return long.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<long>());
        }
    }

    public class Uint64Converter : IArgumentConverter<ulong>
    {
        Task<Optional<ulong>> IArgumentConverter<ulong>.ConvertAsync(string value, CommandContext ctx)
        {
            return ulong.TryParse(value, NumberStyles.Integer, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<ulong>());
        }
    }

    public class Float32Converter : IArgumentConverter<float>
    {
        Task<Optional<float>> IArgumentConverter<float>.ConvertAsync(string value, CommandContext ctx)
        {
            return float.TryParse(value, NumberStyles.Number, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<float>());
        }
    }

    public class Float64Converter : IArgumentConverter<double>
    {
        Task<Optional<double>> IArgumentConverter<double>.ConvertAsync(string value, CommandContext ctx)
        {
            return double.TryParse(value, NumberStyles.Number, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<double>());
        }
    }

    public class Float128Converter : IArgumentConverter<decimal>
    {
        Task<Optional<decimal>> IArgumentConverter<decimal>.ConvertAsync(string value, CommandContext ctx)
        {
            return decimal.TryParse(value, NumberStyles.Number, ctx.CommandsNext.DefaultParserCulture, out var result)
                ? Task.FromResult(Optional.FromValue(result))
                : Task.FromResult(Optional.FromNoValue<decimal>());
        }
    }
}
