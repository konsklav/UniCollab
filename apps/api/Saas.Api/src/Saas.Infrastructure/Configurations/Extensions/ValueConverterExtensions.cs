using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saas.Domain.Common;

namespace Saas.Infrastructure.Configurations.Extensions;

internal static class ValueConverterExtensions
{
    public static PropertyBuilder<Title> HasTitle<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, Title>> titlePropertyAccess) where T : class
    {
        return builder.Property(titlePropertyAccess)
            .HasConversion(
                convertToProviderExpression: title => title.Value,
                convertFromProviderExpression: str => Title.Create(str))
            .HasColumnType($"nvarchar({Title.MaxLength})");
    }
}