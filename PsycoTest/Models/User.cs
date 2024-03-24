using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PsycoTest.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, укажите свою группу: ")]
        [Display(Name = "Укажите свою группу: ")]

        public int UserGroupId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите логин!")]
        [Display(Name = "Введите логин: ")]
        public string UserLogin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, введите пароль!")]
        [Display(Name = "Введите пароль: ")]
        public string UserPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, укажите имя!")]
        [Display(Name = "Введите имя: ")]
        public string UserFirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, укажите фамилию!")]
        [Display(Name = "Введите фамилию: ")]
        public string UserLastName { get; set; } 

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, укажите отчество!")]
        [Display(Name = "Введите отчество: ")]
        public string UserPatronymic { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пожалуйста, укажите свою дату рождения!")]
        [Display(Name = "Укажите дату рождения: ")]
        public DateTime UserDateBirth { get; set; }

        public virtual ICollection<Result> Results { get; set; } = new List<Result>();

        public virtual Group UserGroup { get; set; }
    }

}