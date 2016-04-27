using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SearchForKnowledge.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}