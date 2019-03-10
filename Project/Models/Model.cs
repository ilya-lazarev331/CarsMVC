using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Model
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int BrandID { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Body> Bodies { get; set; }

        public Model()
        {
            Bodies = new List<Body>();
        }
    }
}