using Microsoft.Extensions.Configuration;

namespace Persistense.Provider;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddCustomConfiguration(this IConfigurationBuilder builder)
    {
        var tempConfig = builder.Build();
        var connectionString = tempConfig.GetConnectionString("SQL");

        return builder.Add(new ConfigurationSource(connectionString));
    }
}
