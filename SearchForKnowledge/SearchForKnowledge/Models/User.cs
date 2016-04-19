using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchForKnowledge.Models
{
    public class User
    {
        private string username { get; set; }
        private string password { get; set; }
        private string schoolName { get; set; }
        private string country { get; set; }
        private string city { get; set; }

        public User(string userName, string password, string schoolName, string country, string city)
        {
            this.username = username;
            this.password = password;
            this.schoolName = schoolName;
            this.country = country;
            this.city = city;
        }
    }
}