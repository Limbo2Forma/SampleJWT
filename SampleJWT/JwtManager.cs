using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SampleJWT {
    public static class JwtManager {
 
        private const string Secret = "//56AG0AXgA9AFUATQBgACMAOgBgAEgAIgBXADwAIgB7AHkAXwBiAHUAawBgAE4AcgAjAHsAZgA8ADwAdgBkACYARQBXAEgAPABrAGMASwBlADwAQAAkAH4AUAAiADoAWgBrAHEANwBvADYAaQBAADoATABPAFsAcwA6AG0ANQA=";

        public static string GenerateToken(string username, int expireMinutes = 10) {
            var key = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var key = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters() {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, 
                    validationParameters, out securityToken);

                return principal;
            }

            catch (Exception) {
                return null;
            }
        }
    }
}