using KorepetycjeNaJuz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace KorepetycjeNaJuz.Infrastructure.Auth
{
    public class OAuthService : IOAuthService
    {
        private readonly TokenAuthOptions _tokenAuthOptions;
        public OAuthService(TokenAuthOptions tokenAuthOptions)
        {
            this._tokenAuthOptions = tokenAuthOptions;
        }
        public string GetUserAuthToken(string userName, string userId)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userName, "TokenAuth"),
                new[] { new Claim("UserId", userId, ClaimValueTypes.String) }
                );

            Microsoft.IdentityModel.Tokens.SecurityToken securityToken = handler.CreateToken(
                new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Issuer = this._tokenAuthOptions.Issuer,
                    Audience = this._tokenAuthOptions.Audience,
                    SigningCredentials = this._tokenAuthOptions.SigningCredentials,
                    Subject = identity,
                    Expires = DateTime.UtcNow.Add(this._tokenAuthOptions.LifeSpan)
                });

            return handler.WriteToken(securityToken);
        }

        public IDictionary<string,string> GetClaims(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token).Claims.ToDictionary(x => x.Type, x => x.Value);
        }        
    }
}
