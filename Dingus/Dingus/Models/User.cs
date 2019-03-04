namespace Dingus.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}
