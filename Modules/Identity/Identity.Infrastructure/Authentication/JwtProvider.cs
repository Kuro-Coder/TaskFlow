using Azure.Core;
using Identity.Application.Abstractions;
using Identity.Application.Users.Commands.Login;
using Identity.Domain.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(
        IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public TokenResponse GenerateToken(
        User user)
    {
        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _options.SecretKey));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var expiresAtUtc =
            DateTime.UtcNow.AddMinutes(
                _options.ExpirationInMinutes);

        var token =
            new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _options.ExpirationInMinutes),
                signingCredentials: credentials);

        var accessToken =
            new JwtSecurityTokenHandler()
                .WriteToken(token);

        return new TokenResponse(
            accessToken,
            expiresAtUtc);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }
}