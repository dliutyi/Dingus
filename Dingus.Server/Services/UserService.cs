using Dingus.Server.Contexts;
using Dingus.Server.Helpers;
using Dingus.Server.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dingus.Server.Services
{
    public class UserService
    {
        AppSettings _appSettings;
        DingusContext _dingusContext;
        public UserService(IOptions<AppSettings> settings, DingusContext context)
        {
            _appSettings = settings.Value;
            _dingusContext = context;
        }

        public User Auth(string login, string password)
        {
            User user = _dingusContext.Users.SingleOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                return null;
            }

            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(_appSettings.ExpiredTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";

            return user;
        }

        public async Task Register(User user)
        {
            _dingusContext.Users.Add(user);
            await _dingusContext.SaveChangesAsync();
        }
    }
}
