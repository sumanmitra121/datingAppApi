
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TestApi.Entities;
using TestApi.Interfaces;

namespace TestApi.Services
{
     public class TokenService : ITokenService
    {

        private readonly  SymmetricSecurityKey _Key;
        public TokenService(IConfiguration _config)
        {
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
        }

        public string CreateToken(AppUser _user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,_user.Email),
                // new Claim(JwtRegisteredClaimNames.NameId,_user.UserName)
            };
            var creds = new SigningCredentials(_Key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}