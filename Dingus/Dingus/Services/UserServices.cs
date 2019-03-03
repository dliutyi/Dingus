using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Dingus.Helpers;
using Dingus.Models;

namespace Dingus.Services
{
    public class UserServices
    {
        private NetServices _service;

        public UserServices() => _service = new NetServices();

        public async Task<User> Auth(User user)
        {
            User copyUser = user.Clone() as User;
            copyUser.Salt = await GetSalt(copyUser.Login);
            copyUser.Password = HashPassword(copyUser.Password, copyUser.Salt);
            return await _service.GetDeserializedObject<User>($"{AppSettings.CurrentDomain}/api/account/auth", HttpMethod.Post, copyUser);
        }

        public async Task<User> Register(User user)
        {
            user.Salt = Guid.NewGuid().ToString("N");
            user.Password = HashPassword(user.Password, user.Salt);
            return await _service.GetDeserializedObject<User>($"{AppSettings.CurrentDomain}/api/account/register", HttpMethod.Post, user);
        }

        private async Task<string> GetSalt(string login) => await _service.GetRawObject($"{AppSettings.CurrentDomain}/api/users/salt/{login}");

        private string HashPassword(string password, string salt)
        {
            SHA256 sha256 = new SHA256Managed();
            byte[] bytes = UTF8Encoding.UTF8.GetBytes($"{password}{salt}");
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; ++i)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
