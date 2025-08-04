using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sports.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sports.DTO
{
    public class Token
    {
        private readonly IConfiguration _configuration;
        private readonly DB _context;
        public Token(IConfiguration configuration , DB context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<string> GenerateToken(Academy academy)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var information = await _context.Academies.FindAsync(academy.Id);
                var role = information!.Role;
                
                var claims = new List<Claim>
                {
                    new Claim("Id", academy.Id.ToString()),
                    new Claim("AcademyEmail", academy.AcademyEmail!),
                    new Claim("AcademyName", academy.AcademyName!),
                    new Claim("AcademyPhone", academy.AcademyPhone!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Role",role!)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: signinCredentials
                );
                return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException(ex.Message);
            }
        }
    }

}
