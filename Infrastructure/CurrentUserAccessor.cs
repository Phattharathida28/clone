using Application.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Infrastructure;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    private string ExecuteContext(Func<string> action)
    {
        if (_httpContextAccessor.HttpContext == null) return null;
        else return action();
    }

    public long UserId
    {
        get
        {
            Int64.TryParse(this.ExecuteContext(() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)), out long userId);
            return userId;
        }

    }
    public string UserName
    {
        get => this.ExecuteContext(() => _httpContextAccessor.HttpContext.User.FindFirstValue(JwtClaimTypes.PreferredUserName));
    }

    public string Company
    {
        get
        {
            return this.ExecuteContext(() =>
            {
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("company", out var company);
                return company;
            });
        }
    }

    public string Organization
    {
        get
        {
            return this.ExecuteContext(() =>
            {
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("organization", out var organization);
                return organization;
            });
        }
    }

    public string ProgramCode
    {
        get
        {
            return this.ExecuteContext(() =>
            {
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("program", out var program);
                return program;
            });
        }
    }

    public string Language
    {
        get
        {
            return this.ExecuteContext(() =>
            {
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("language", out var language);
                return language;
            });
        }
    }
}
