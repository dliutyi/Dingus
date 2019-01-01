using System;
using System.Collections.Generic;
using System.Text;

using Dingus.Models;

namespace Dingus.Services
{
    public class UserServices
    {
        public List<User> GetUsers()
        {
            List<User> users = new List<User>
            {
                new User { Login = "admin", Password = "123456789", FirstName = "admin", LastName = "admin" }
            };
            return users;
        }
    }
}
