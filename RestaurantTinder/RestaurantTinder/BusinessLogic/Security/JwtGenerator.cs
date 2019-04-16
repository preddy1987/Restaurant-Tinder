using Microsoft.IdentityModel.Tokens;
using RestaurantTinder.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantService.Security
{
    /// <summary>
    /// Represents a JWT generator.
    /// </summary>
    public class JwtGenerator : ITokenGenerator
    {
        private readonly string secret;

        /// <summary>
        /// Creates a new JWT generator.
        /// </summary>
        /// <param name="secret">The app's secret to use when signing the token.</param>
        public JwtGenerator(string secret)
        {
            this.secret = secret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GenerateToken(UserItem user)
        {
            // Creates the sample token to send to the client.
            /*
             {
              "sub": "joe@techelevator.com",
              "rol": "User",
              "iat": 1552613405,
              "exp": 1552617005
            }
            */

            var claims = new[]
            {
                new Claim("sub", user.Username),
                new Claim("rol", user.RoleId),
                new Claim("id", user.Id.ToString()),
                new Claim("zip", user.ZipCode.ToString()),
                new Claim("iat", DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}
