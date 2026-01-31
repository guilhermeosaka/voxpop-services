using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Voxpop.Packages.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> WithUtcConverter<TProperty>(this PropertyBuilder<TProperty> builder)
    {
        var utcConverter = new ValueConverter<DateTimeOffset, DateTimeOffset>(
            v => v.ToUniversalTime(),
            v => v.UtcDateTime
        );

        return builder.HasConversion(utcConverter);
    }
}