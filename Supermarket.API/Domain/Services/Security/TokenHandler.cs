using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Tokens;

namespace Supermarket.API.Domain.Services.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IPasswordHasher _passwordHasher;
        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _signingConfigurations = signingConfigurations;
            _tokenOptions = tokenOptionsSnapshot.Value;
        }
        public AccessToken CreateAccessToken(User user)
        {
            var refreshtoken = BuildRefreshToken(user);
            var accesstoken = BuildAccessToken(user, refreshtoken);
            _refreshTokens.Add(refreshtoken);
            return accesstoken;

        }

        private RefreshToken BuildRefreshToken(User user)
        {
            var refreshToken = new RefreshToken(
                token: _passwordHasher.HashPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
            );

            return refreshToken;

        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var refreshtoken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshtoken != null)
            {
                _refreshTokens.Remove(refreshtoken);
            }
            return refreshtoken;
        }

        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            return claims;
        }
    }
}