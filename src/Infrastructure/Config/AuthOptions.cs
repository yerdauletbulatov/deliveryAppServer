using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Config
{
    public class AuthOptions
    {
        public const string JwtSettings = "JwtSettings";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifeTimeTokenInMinute { get; set; }
        public int LifeTimeRefreshTokenInYear { get; set; }
        public string SecretKey { get; set; }
    }
}