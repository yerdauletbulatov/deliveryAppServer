using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.TokenInterfaces;
using Infrastructure.AppData.Identity;
using Infrastructure.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.TokenServices
{
    public class TokenService : IGenerateToken, IRefreshToken
    {
        private readonly AppIdentityDbContext _identityDb;
        private readonly AuthOptions _options;
        public int LifeTimeRefreshTokenInYear { get; }
        public TokenService(AppIdentityDbContext identityDb, IOptions<AuthOptions> options)
        {
            _identityDb = identityDb;
            _options = options.Value;
            LifeTimeRefreshTokenInYear = _options.LifeTimeRefreshTokenInYear;
        }

        public string CreateAccessToken(User user) => 
            new JwtSecurityTokenHandler()
                .WriteToken(
                    new JwtSecurityToken(
                        _options.Issuer,
                        _options.Audience,
                        notBefore: DateTime.UtcNow,
                        claims: new List<Claim> { new(ClaimTypes.UserData, user.Id) },
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey)),
                            SecurityAlgorithms.HmacSha256),
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_options.LifeTimeTokenInMinute)))
                );

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<ActionResult> RefreshTokenAsync(RefreshTokenInfo tokenInfo)
        {
            try
            {
                var user = await _identityDb.Users.FirstAsync(u => u.RefreshToken == tokenInfo.RefreshToken &&
                                                        u.RefreshTokenExpiryTime >= DateTime.Now);
                return new OkObjectResult(new {AccessToken = CreateAccessToken(user)});
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}