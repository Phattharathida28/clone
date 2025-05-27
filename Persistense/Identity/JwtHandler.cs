using System;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Persistense.Identity;

public class JwtResponse
{
    public string Token { get; set; }
    public long ExpiresAt { get; set; }
}

public interface IJwtHandler
{
    JwtResponse CreateToken();
    bool ValidateToken(string token, string publicKey);
}

public static class TypeConverterExtension
{
    public static byte[] ToByteArray(this string value) => Convert.FromBase64String(value);
}

public class JwtHandler : IJwtHandler
{

    public JwtHandler()
    {

    }

    public JwtResponse CreateToken() => throw new NotImplementedException();

    public bool ValidateToken(string token, string publicKey)
    {
        using RSA rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);

        try
        {
            JwtSecurityTokenHandler handler = new();

            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateLifetime = false,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ValidateIssuer = false,
                ValidateAudience = false,
                CryptoProviderFactory = new CryptoProviderFactory()
                {
                    CacheSignatureProviders = false
                }
            }, out SecurityToken validatedToken);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}
