using Microsoft.Extensions.Configuration;

namespace Persistense.Provider;

internal class ConfigurationSource : IConfigurationSource
{
    private readonly string _connectionString;
    public ConfigurationSource(string connectionString) => _connectionString = connectionString;
    public IConfigurationProvider Build(IConfigurationBuilder builder) => new CustomConfigurationProvider(_connectionString);
}
