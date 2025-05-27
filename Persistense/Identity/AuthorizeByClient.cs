using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Runtime.Caching;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Domain.Entities.SU;

namespace Persistense.Identity;

public class AuthorizeByClientAttribute : Attribute, IAuthorizationFilter
{
    readonly string[] _clients;
    private static MemoryCache _memoryCache;

    static AuthorizeByClientAttribute() => _memoryCache = new MemoryCache("ClientIdCache");

    public AuthorizeByClientAttribute(params string[] clients) => this._clients = clients;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            if (_clients.Count() == 0) throw new UnauthorizedAccessException();

            string token = context.HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            string id = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "client_id")?.Value;

            string publicKey = string.Empty;

            if (!_memoryCache.Contains(id))
            {
                IConfiguration configuration = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                string connectionString = configuration.GetConnectionString("SQL");
                ApiClient client = null;
                using (var apiContext = new ApiClientContext(connectionString))
                {
                    client = apiContext.Set<ApiClient>().FirstOrDefault(x => x.ClientId == id);
                }

                if (client != null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.Priority = CacheItemPriority.NotRemovable;
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(2);
                    _memoryCache.Add(id, client?.PublicKey, policy);
                    publicKey = client?.PublicKey;
                }
            }
            else
            {
                publicKey = _memoryCache?.Get(id) + "";
            }

            IJwtHandler jwtHandler = context.HttpContext.RequestServices.GetService(typeof(IJwtHandler)) as IJwtHandler;
            bool result = jwtHandler.ValidateToken(token, publicKey);

            if (!result) throw new UnauthorizedAccessException();

        }
        catch (Exception ex)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
