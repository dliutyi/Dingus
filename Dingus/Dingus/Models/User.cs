using System;
using System.Collections.Generic;
using System.Text;

namespace Dingus.Models
{
    public class User : IEquatable<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(User other)
        {
            if(!(other is User))
            {
                return false;
            }
            return this == other;
        }

        public static bool operator ==(User user1, User user2)
        {
            return user1.Login == user2.Login && user1.Password == user2.Password;
        }

        public static bool operator !=(User user1, User user2)
        {
            return user1.Login != user2.Login || user1.Password != user2.Password;
        }


    }
}
