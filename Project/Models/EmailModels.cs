using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class EmailModels
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string From { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string To { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Compare("Password")]
        public string Password { get; set; }
        public string Body { get; set; }
    }
}