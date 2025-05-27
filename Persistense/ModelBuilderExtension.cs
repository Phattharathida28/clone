using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Persistense;

public static class ModelBuilderExtension
{
    public static string ToLowercaseNamingConvention(this string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        Match startUnderscores = Regex.Match(input, @"^_+");

        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    public static IEnumerable<IMutableEntityType> EntityTypes(this ModelBuilder builder) => builder.Model.GetEntityTypes();

    public static IEnumerable<IMutableProperty> Properties(this ModelBuilder builder) => builder.EntityTypes().SelectMany(entityType => entityType.GetProperties());

    public static IEnumerable<IMutableProperty> Properties<T>(this ModelBuilder builder) => builder.EntityTypes().SelectMany(entityType => entityType.GetProperties().Where(x => x.ClrType == typeof(T)));

    public static void Configure(this IEnumerable<IMutableEntityType> entityTypes, Action<IMutableEntityType> convention)
    {
        foreach (var entityType in entityTypes) convention(entityType);
    }

    public static void Configure(this IEnumerable<IMutableProperty> propertyTypes, Action<IMutableProperty> convention)
    {
        foreach (var propertyType in propertyTypes) convention(propertyType);
    }
}
