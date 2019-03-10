using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleID { get; set; }

        public Role Role { get; set; }
    }
}