using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Car
    {
        public int ID { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public int Year { get; set; }

        public int Mileage { get; set; }

        [Required]
        public string Color { get; set; }

        public int Power { get; set; }        

        public int Price { get; set; }

        public string Description { get; set; }
                
        public string Photo { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int CityID { get; set; }
        public virtual City City { get; set; }

        public int BrandID { get; set; }
        public virtual Brand Brand { get; set; }

        public int ModelID { get; set; }
        public virtual Model Model { get; set; }

        public int TransmissionID { get; set; }
        public virtual Transmission Transmission { get; set; }

        public int BodyID { get; set; }
        public virtual Body Body { get; set; }        
    }
}