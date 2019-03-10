using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Body
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }

        public Body()
        {
            Models = new List<Model>();
        }
    }
}