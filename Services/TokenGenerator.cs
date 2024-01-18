using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RecipeManager.Models;

namespace RecipeManager.Services
{
    public class TokenGenerator
    {
        public static string GenerateJWT(UserEntity user)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("q2Xf22pvuYhDGBjRTiNuA7f4pzSWfJNGizifG21Wpxki7VWRxH"));
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = [];

            if (user.Role == 1)
                claims.Add(new Claim(ClaimTypes.Role, "Standard"));
            if (user.Role == 2)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            claims.Add(new Claim("UserId", user.Id.ToString()));

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5218",
                audience: "http://localhost:5218",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}