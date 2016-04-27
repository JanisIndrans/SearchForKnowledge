using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SearchForKnowledge.ViewModels
{
    public class UsersIndex
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

    public class UsersNew {
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(128)]
        public string SchoolName { get; set; }
        [Required, MaxLength(128)]
        public string Country { get; set; }
        [Required, MaxLength(128)]
        public string City { get; set; }
    }

    public class UsersLogin {
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UsersShowHash
    {
        public string Hash { get; set; }
    }

    public class AdminPage {
        [MaxLength(128)]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [ MaxLength(128)]
        public string SchoolName { get; set; }
        [MaxLength(128)]
        public string Country { get; set; }
        [MaxLength(128)]
        public string City { get; set; }
    }
}