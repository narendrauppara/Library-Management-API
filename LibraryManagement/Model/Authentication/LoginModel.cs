using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Model.Authentication
{
    public class LoginModel
    {

        [Required(ErrorMessage = "User mail id is required")]
        public string UserMailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public string Password { get; set; }


    }
}
