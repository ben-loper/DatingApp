using Database.Data;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;

        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
        }


        public string CreateToken(AppUser user)
        {
            var tokenKey = _config["TokenKey"] ?? throw new MissingTokenKeyException("Cannot find token key from config using key [TokenKey]");

            // This is because of the signature we are using requires 64 character string
            if (tokenKey.Length < 64) throw new TokenKeyTooShortException("Token key length is less than 64 characters");

            // Symmetric => One key to encrypt and decrypt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            // Claims for the user => username, DOB, etc.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            // The signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Descriptor that takes the identity, when it expires, and the signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Returns the string value of the token 
            return tokenHandler.WriteToken(token);
        }
    }
}
