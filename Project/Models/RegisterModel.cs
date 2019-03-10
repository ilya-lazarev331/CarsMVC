using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class RegisterModel
    {
        [Display(Name = "Логин")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 15 символов")]
        [RegularExpression(@"^[a-zA-ZА-Яа-я]+$", ErrorMessage = "Логин должен состоять только из букв")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Слишком длинный email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина пароля должна быть от 3 до 30 символов")]
        [RegularExpression(@"^[a-zA-ZА-Яа-я0-9]+$", ErrorMessage = "Пароль должен состоять только из букв и цифр")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /*[Compare("Password", ErrorMessage = "Пароли не совпадают")]
          [DataType(DataType.Password)]
          public string PasswordConfirm { get; set; }*/
    }
}