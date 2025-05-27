using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Web.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class ConfigurationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ConfigurationController(IConfiguration configuration) => _configuration = configuration;

    public IActionResult Get()
    {
        Dictionary<string, string> config = _configuration.GetSection("Configuration").GetChildren().ToDictionary(x => x.Key, x => x.Value);
        IEnumerable<IConfigurationSection> language = _configuration.GetSection("Languages").GetChildren();
        language = language.OrderBy(l => l.Key.ToLower() == "en" ? 1 : (l.Key.ToLower() == "th" ? 2 : 99)).ThenBy(l => l.Key);
        string configJson = JsonSerializer.Serialize(config);

        return Ok(new { configuration = JsonSerializer.Deserialize<object>(configJson), languages = language });
    }
}
