using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dingus.Helpers;
using Dingus.Models;
using Newtonsoft.Json;

namespace Dingus.Services
{
    public class UserServices
    {
        public async Task<User> Auth(User user)
        {
            User copyUser = user.Clone() as User;

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            copyUser.Salt = await GetSalt(client, copyUser.Login);
            copyUser.Password = HashPassword(copyUser.Password, copyUser.Salt);

            Uri uri = new Uri(string.Format("{0}/api/account/auth", AppSettings.CurrentDomain));
            try
            {
                StringContent data = new StringContent(JsonConvert.SerializeObject(copyUser), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, data);
                if (response.IsSuccessStatusCode)
                {
                    string readResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(readResponse);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> Register(User user)
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            user.Salt = Guid.NewGuid().ToString("N");
            user.Password = HashPassword(user.Password, user.Salt);

            Uri uri = new Uri(string.Format("{0}/api/account/register", AppSettings.CurrentDomain));
            try
            {
                StringContent data = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, data);
                if (response.IsSuccessStatusCode)
                {
                    string readResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(readResponse);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GetSalt(HttpClient client, string login)
        {
            Uri uri = new Uri(string.Format("{0}/api/users/salt/{1}", AppSettings.CurrentDomain, login));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }    

        private string HashPassword(string password, string salt)
        {
            string saltedPassword = String.Concat(password, salt);
            SHA256 sha256 = new SHA256Managed();
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(saltedPassword);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
