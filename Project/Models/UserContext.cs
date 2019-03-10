using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Project.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Body> Bodies { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Body>().HasMany(c => c.Models)
                .WithMany(s => s.Bodies)
                .Map(t => t.MapLeftKey("BodyId")
                .MapRightKey("ModelId")
                .ToTable("BodyModel"));

            modelBuilder.Entity<Car>()
                .HasRequired(c => c.Model)
                .WithMany()
                .WillCascadeOnDelete(false);            
        }        
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Roles.Add(new Role { ID = 1, Name = "admin" });
            db.Roles.Add(new Role { ID = 2, Name = "user" });

            db.Cities.Add(new City { ID = 1, Name = "Саратов" });
            db.Cities.Add(new City { ID = 2, Name = "Москва" });
            db.Cities.Add(new City { ID = 3, Name = "Энгельс" });
            db.Cities.Add(new City { ID = 4, Name = "Санкт-Петербург" });
            db.Cities.Add(new City { ID = 5, Name = "Уфа" });
            db.Cities.Add(new City { ID = 6, Name = "Оренбург" });
            db.Cities.Add(new City { ID = 7, Name = "Самара" });
            db.Cities.Add(new City { ID = 8, Name = "Казань" });

            db.Users.Add(new User
            {
                ID = 1,
                Email = "somemail@gmail.com",
                Password = "123456",
                Login = "asd",
                RoleID = 1
            });

            db.Transmissions.Add(new Transmission { ID = 1, Name = "Механическая" });
            db.Transmissions.Add(new Transmission { ID = 2, Name = "Автоматическая" });
            db.Transmissions.Add(new Transmission { ID = 3, Name = "Робот" });

            Body s1 = new Body { ID = 1, Name = "Хетчбек" };
            Body s2 = new Body { ID = 2, Name = "Купе" };
            Body s3 = new Body { ID = 3, Name = "Седан" };
            Body s4 = new Body { ID = 4, Name = "Универсал" };
            Body s5 = new Body { ID = 5, Name = "Внедорожник" };

            db.Bodies.Add(s1);
            db.Bodies.Add(s2);
            db.Bodies.Add(s3);
            db.Bodies.Add(s4);
            db.Bodies.Add(s5);

            db.Brands.Add(new Brand { ID = 1, Name = "Opel" });
            db.Brands.Add(new Brand { ID = 2, Name = "Mazda" });
            db.Brands.Add(new Brand { ID = 3, Name = "Ford" });
            db.Brands.Add(new Brand { ID = 4, Name = "BMW" });
            db.Brands.Add(new Brand { ID = 5, Name = "Mercedes" });
            db.Brands.Add(new Brand { ID = 6, Name = "Audi" });            
            db.Brands.Add(new Brand { ID = 7, Name = "Toyota" });

            db.Models.Add(new Model {
                ID = 1,
                Name = "Astra",
                Year = 1991,
                BrandID = 1,
                Bodies = new List<Body>() { s1, s2, s3, s4 }
            });
            db.Models.Add(new Model
            {
                ID = 2,
                Name = "3",
                Year = 2003,
                BrandID = 2,
                Bodies = new List<Body>() { s1, s3 }
            });
            db.Models.Add(new Model
            {
                ID = 3,
                Name = "Focus",
                Year = 1998,
                BrandID = 3,
                Bodies = new List<Body>() { s1, s2, s3, s4 }
            });
            db.Models.Add(new Model
            {
                ID = 4,
                Name = "Mondeo",
                Year = 1993,
                BrandID = 3,
                Bodies = new List<Body>() { s3 }
            });
            db.Models.Add(new Model
            {
                ID = 5,
                Name = "Mustang",
                Year = 1964,
                BrandID = 3,
                Bodies = new List<Body>() { s2 }
            });
            db.Models.Add(new Model
            {
                ID = 6,
                Name = "6",
                Year = 2002,
                BrandID = 2,
                Bodies = new List<Body>() { s3 }
            });
            db.Models.Add(new Model
            {
                ID = 7,
                Name = "X6",
                Year = 2007,
                BrandID = 4,
                Bodies = new List<Body>() { s5 }
            });
            db.Models.Add(new Model
            {
                ID = 8,
                Name = "M5",
                Year = 1985,
                BrandID = 4,
                Bodies = new List<Body>() { s1, s2, s3, s4 }
            });
            db.Models.Add(new Model
            {
                ID = 9,
                Name = "C-klasse",
                Year = 1993,
                BrandID = 5,
                Bodies = new List<Body>() { s2, s3, s4 }
            });
            db.Models.Add(new Model
            {
                ID = 10,
                Name = "GL-klasse",
                Year = 2006,
                BrandID = 5,
                Bodies = new List<Body>() { s5 }
            });
            db.Models.Add(new Model
            {
                ID = 11,
                Name = "A3",
                Year = 1996,
                BrandID = 6,
                Bodies = new List<Body>() { s1, s2, s3, s4 }
            });
            db.Models.Add(new Model
            {
                ID = 12,
                Name = "RS 7",
                Year = 2013,
                BrandID = 6,
                Bodies = new List<Body>() { s3 }
            });
            db.Models.Add(new Model
            {
                ID = 13,
                Name = "Land Cruiser",
                Year = 1960,
                BrandID = 7,
                Bodies = new List<Body>() { s5 }
            });
            db.Models.Add(new Model
            {
                ID = 14,
                Name = "Camry",
                Year = 1983,
                BrandID = 7,
                Bodies = new List<Body>() { s3, s4 }
            });
            db.Models.Add(new Model
            {
                ID = 15,
                Name = "Corolla",
                Year = 1974,
                BrandID = 7,
                Bodies = new List<Body>() { s1, s2, s3, s4}
            });

            db.Cars.Add(new Car
            {
                ID = 1,
                Phone = "892722211133",
                Date = DateTime.Now,
                Year = 2008,
                Mileage = 90000,
                Color = "Черный",
                Power = 140,                
                Price = 350000,
                Description = "Крутая тачка",
                Photo = "/Content/Images/Cars/astr.jpg",
                TransmissionID = 1,
                BodyID = 2,
                BrandID = 1,
                ModelID = 1,
                CityID = 1,
                UserID = 1
            });

            db.Cars.Add(new Car
            {
                ID = 2,
                Phone = "892722211133",
                Date = DateTime.Now,
                Year = 2009,
                Mileage = 80000,
                Color = "Серебристый",
                Power = 120,                
                Price = 340000,
                Description = "Хорошая машина",
                Photo = "/Content/Images/Cars/focs.jpg",
                TransmissionID = 2,
                BodyID = 3,
                BrandID = 3,
                ModelID = 3,
                CityID = 2,
                UserID = 1
            });

            db.Cars.Add(new Car
            {
                ID = 3,
                Phone = "892722211133",
                Date = DateTime.Now,
                Year = 2007,
                Mileage = 100000,
                Color = "Серый",
                Power = 106,                
                Price = 400000,
                Description = "Суперская тачка",
                Photo = "/Content/Images/Cars/mazd.jpg",
                TransmissionID = 2,
                BodyID = 3,
                BrandID = 2,
                ModelID = 2,
                CityID = 1,
                UserID = 1
            });
            db.SaveChanges();
            //base.Seed(db);
        }
    }
}