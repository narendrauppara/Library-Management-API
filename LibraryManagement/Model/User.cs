using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Model
{
    public class User
    {
        [Key]
        public string UserEmailId { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        //public string  Role { get; set; }
    }
}
