using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Linq;

namespace Persistense.Provider;

internal class CustomConfigurationProvider : ConfigurationProvider
{
    private readonly string _connectionString;

    public CustomConfigurationProvider(string connectionString) => _connectionString = connectionString;

    public override void Load()
    {
        using (var con = new NpgsqlConnection(_connectionString))
        {
            con.Open();
            var configs = con.Query<Configuration>(@"select concat(parameter_group_code,':',parameter_code) as Key,parameter_value as Value
                                                   from su.parameter sp
                                                   where parameter_group_code = @Group ", new { Group = "Configuration" });
            var languages = con.Query<Configuration>(@"SELECT  concat('Languages',':',d.language_code) as Key ,l.language_name as Value
                                                         FROM  db.language as d 
                                                         inner join db.language_lang as l  on l.language_code = d.language_code 
                                                         and l.language_code_forname = d.language_code 
                                                         where active = true ");
            Data = configs.Concat(languages).ToDictionary(c => c.Key, c => c.Value, StringComparer.OrdinalIgnoreCase);

            con.Close();
        }
    }
}
