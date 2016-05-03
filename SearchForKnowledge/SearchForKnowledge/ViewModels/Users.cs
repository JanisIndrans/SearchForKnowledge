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

    public class UsersNew
    {

        public string DuplicateUserMessage { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?!.*(_|[^\w])).+$", ErrorMessage =
            "Password must contain both lower and upper case letters and a nummeric digit! Password " +
            "must not contain non-alphanumeric letters!")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?!.*(_|[^\w])).+$", ErrorMessage =
            "Password must contain both lower and upper case letters and a nummeric digit! Password " +
            "must not contain non-alphanumeric letters!")]
        public string ConfirmPassword { get; set; }

        [Required, MaxLength(128), MinLength(3)]
        public string Username { get; set; }
        [Required, MaxLength(128)]
        public string SchoolName { get; set; }
        [Required, MaxLength(128)]
        public string Country { get; set; }
        [Required, MaxLength(128)]
        public string City { get; set; }

        public string Type { get; set; }
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

        [Required, MaxLength(128)]
        public string Type { get; set; }
    }
}