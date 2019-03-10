using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Models
{
    public class CarModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Display(Name = "Номер телефона")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неправильный формат номера")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Год выпуска")]
        [Range(0, 2017, ErrorMessage = "Неккоректный год")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Год должен быть введен цифрами")]
        [Required]
        public int Year { get; set; }

        [Display(Name = "Пробег")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Пробег должен быть введен цифрами")]
        [Required]
        public int Mileage { get; set; }

        [Display(Name = "Цвет автомобиля")]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Цвет должен быть введен кириллицей")]
        [Required]
        public string Color { get; set; }

        [Display(Name = "Мощность мотора")]
        [Range(20, 2000, ErrorMessage = "Неккоректная мощность")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Мощность должена быть введена цифрами")]
        [Required]
        public int Power { get; set; }

        [Display(Name = "Цена автомобиля")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Цена должена быть введена цифрами")]
        [Required]
        public int Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public string Photo { get; set; }

        public int CityID { get; set; }
        

        public int BrandID { get; set; }
        

        public int ModelID { get; set; }
        

        public int TransmissionID { get; set; }
        

        public int BodyID { get; set; }
        

    }
}