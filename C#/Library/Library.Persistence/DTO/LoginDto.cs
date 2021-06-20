using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Persistence.DTO
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
