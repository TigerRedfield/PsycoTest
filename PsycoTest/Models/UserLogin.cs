using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PsycoTest.Models
{
    public class UserLogin
    {
        public int UserId { get; set; } 
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите логин!")]
        [Display(Name = "Введите логин")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите пароль!")]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }

      
        public string UserFirstName { get; set; }

       
        public string UserLastName { get; set; }

        
        public string UserPatronymic { get; set; }
    }
}