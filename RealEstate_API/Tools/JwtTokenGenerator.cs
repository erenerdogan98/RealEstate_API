using Microsoft.IdentityModel.Tokens;
using RealEstate_API.Dtos.JWTDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate_API.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetChecktAppUserDto model)
        {
            // Creating Claims
            var claims = new List<Claim>();

            if (!string.IsNullOrWhiteSpace(model.Role))
                claims.Add(new Claim(ClaimTypes.Role, model.Role));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(model.UserName))
                claims.Add(new Claim(ClaimTypes.Name, model.UserName));

            // JWT settings
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(JwtTokenDefault.Expire),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefault.Key)),
                    SecurityAlgorithms.HmacSha256
                ),
                Issuer = JwtTokenDefault.ValidIssuer,
                Audience = JwtTokenDefault.ValidAudience
            };

            // JWT creator
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(tokenDescriptor);

            // Token to string
            var tokenString = handler.WriteToken(securityToken);

            // Create TokenResponseDto and return it .. 
            var expireDate = tokenDescriptor.Expires ?? DateTime.UtcNow; // Expires null ise, DateTime.UtcNow'i kullan
            return new TokenResponseDto(tokenString, expireDate);
        }
    }
}
