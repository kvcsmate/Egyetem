using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class LoginViewModel
    {
        [DisplayName("Név")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [DisplayName("Név")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordRepeat { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }
    }
}
