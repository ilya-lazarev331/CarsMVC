using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using System.IO;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace Project.Controllers
{
    public class AdvertController: Controller
    {
        UserContext db = new UserContext();        

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            int selectedIndex1 = 2;            
            SelectList brands = new SelectList(db.Brands, "Id", "Name", selectedIndex1);
            SelectList models = new SelectList(db.Models.Where(m => m.BrandID == selectedIndex1), "Id", "Name");
            SelectList bodies = new SelectList(db.Models.FirstOrDefault(t => t.BrandID == selectedIndex1).Bodies.ToList(), "Id", "Name");
            SelectList cities = new SelectList(db.Cities, "Id", "Name");
            SelectList tr = new SelectList(db.Transmissions, "Id", "Name");
            ViewBag.Brands = brands;
            ViewBag.Models = models;
            ViewBag.Bodies = bodies;
            ViewBag.Cities = cities;
            ViewBag.TR = tr;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CarModel model, HttpPostedFileBase uploadImage)
        {
            Model md = db.Models.FirstOrDefault(m => m.ID == model.ModelID);
            if (model.Year != null && md != null)
            {
                if (model.Year < md.Year)
                {
                    string str = "Данная модель выпускалась с " + md.Year + " года";
                    ModelState.AddModelError("Year", str);
                }
            }
            if (ModelState.IsValid)
            {
                string path = null;
                if (uploadImage != null)
                {
                    if (uploadImage.ContentLength > 0)
                    {
                        if (Path.GetExtension(uploadImage.FileName).ToLower() == ".jpg"
                            || Path.GetExtension(uploadImage.FileName).ToLower() == ".png")
                        {
                            string b = uploadImage.FileName.ToLower();
                            while (true)
                            {
                                string[] files1 = Directory.GetFiles(Server.MapPath("~/Content/Images/Cars"), b);
                                if (files1.Length == 0) { break; }
                                else b = "1" + b;
                            }
                            path = Path.Combine(Server.MapPath("~/Content/Images/Cars"), b);
                            uploadImage.SaveAs(path);
                            path = "/Content/Images/Cars/" + b;
                        }
                    }
                }                
                using (UserContext db = new UserContext())
                {
                    User user = db.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
                    db.Cars.Add(new Car
                    {
                        Phone = model.Phone,
                        Year = model.Year,
                        Mileage = model.Mileage,
                        Color = model.Color,
                        Power = model.Power,
                        Price = model.Price,
                        Description = model.Description,
                        BrandID = model.BrandID,
                        ModelID = model.ModelID,
                        BodyID = model.BodyID,
                        TransmissionID = model.TransmissionID,
                        CityID = model.CityID,
                        Photo = path,
                        Date = DateTime.Now,
                        UserID = user.ID
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            int selectedIndex1 = 2;
            int selectedIndex2 = 1;
            SelectList brands = new SelectList(db.Brands, "Id", "Name", selectedIndex1);
            SelectList models = new SelectList(db.Models.Where(m => m.BrandID == selectedIndex1), "Id", "Name", selectedIndex2);
            SelectList bodies = new SelectList(db.Models.FirstOrDefault(t => t.ID == selectedIndex2).Bodies.ToList(), "Id", "Name");
            SelectList cities = new SelectList(db.Cities, "Id", "Name");
            SelectList tr = new SelectList(db.Transmissions, "Id", "Name");
            ViewBag.Brands = brands;
            ViewBag.Models = models;
            ViewBag.Bodies = bodies;
            ViewBag.Cities = cities;
            ViewBag.TR = tr;
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }            
            SelectList brands = new SelectList(db.Brands, "Id", "Name", car.BrandID);
            SelectList models = new SelectList(db.Models.Where(m => m.BrandID == car.BrandID), "Id", "Name", car.ModelID);
            SelectList bodies = new SelectList(db.Models.FirstOrDefault(t => t.BrandID == car.BrandID).Bodies.ToList(), "Id", "Name", car.BodyID);
            SelectList cities = new SelectList(db.Cities, "Id", "Name", car.CityID);
            SelectList tr = new SelectList(db.Transmissions, "Id", "Name", car.TransmissionID);
            ViewBag.Brands = brands;
            ViewBag.Models = models;
            ViewBag.Bodies = bodies;
            ViewBag.Cities = cities;
            ViewBag.TR = tr;
            CarModel cr = new CarModel
            {
                ID = car.ID,
                Phone = car.Phone,
                Year = car.Year,
                Mileage = car.Mileage,
                Color = car.Color,
                Power = car.Power,
                Price = car.Price,
                Description = car.Description,
                Photo = car.Photo,
                BrandID = car.BrandID,
                ModelID = car.ModelID,
                BodyID = car.BodyID,
                TransmissionID = car.TransmissionID,
                CityID = car.CityID,
            };      
            return View(cr);
        }

        [HttpPost]
        public ActionResult Edit(CarModel model, HttpPostedFileBase uploadImage)
        {
            Car car = db.Cars.Find(model.ID);
            Model md = db.Models.FirstOrDefault(m => m.ID == model.ModelID);
            if (model.Year != null && md != null)
            {
                if (model.Year < md.Year)
                {
                    string str = "Данная модель выпускалась с " + md.Year + " года";
                    ModelState.AddModelError("Year", str);
                }
            }
            if (ModelState.IsValid)
            {
                string path = null;
                if (uploadImage != null)
                {
                    if (uploadImage.ContentLength > 0)
                    {
                        if (Path.GetExtension(uploadImage.FileName).ToLower() == ".jpg"
                            || Path.GetExtension(uploadImage.FileName).ToLower() == ".png")
                        {
                            string b = uploadImage.FileName.ToLower();
                            while (true)
                            {
                                string[] files1 = Directory.GetFiles(Server.MapPath("~/Content/Images/Cars"), b);
                                if (files1.Length == 0) { break; }
                                else b = "1" + b;
                            }
                            path = Path.Combine(Server.MapPath("~/Content/Images/Cars"), b);
                            uploadImage.SaveAs(path);
                            path = "/Content/Images/Cars/" + b;
                        }
                    }
                }  
                    car.Phone = model.Phone;
                    car.Year = model.Year;
                    car.Mileage = model.Mileage;
                    car.Color = model.Color;
                    car.Power = model.Power;
                    car.Price = model.Price;
                    car.Description = model.Description;
                    car.BrandID = model.BrandID;
                    car.ModelID = model.ModelID;
                    car.BodyID = model.BodyID;
                    car.TransmissionID = model.TransmissionID;
                    car.CityID = model.CityID;
                    if (path != null) car.Photo = path;
                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();                    
                    return RedirectToAction("Index", "Home");                
            }
            int selectedIndex1 = 2;
            int selectedIndex2 = 1;
            SelectList brands = new SelectList(db.Brands, "Id", "Name", selectedIndex1);
            SelectList models = new SelectList(db.Models.Where(m => m.BrandID == selectedIndex1), "Id", "Name", selectedIndex2);
            SelectList bodies = new SelectList(db.Models.FirstOrDefault(t => t.ID == selectedIndex2).Bodies.ToList(), "Id", "Name");
            SelectList cities = new SelectList(db.Cities, "Id", "Name");
            SelectList tr = new SelectList(db.Transmissions, "Id", "Name");
            ViewBag.Brands = brands;
            ViewBag.Models = models;
            ViewBag.Bodies = bodies;
            ViewBag.Cities = cities;
            ViewBag.TR = tr;
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            User user = db.Users.FirstOrDefault(us => us.Login == User.Identity.Name);
            ViewBag.User = user;
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }            
            return View(car);
        }

        [HttpPost]
        public ActionResult Details(string phone, int id)
        {
            Car car = db.Cars.Find(id);
            User user = db.Users.FirstOrDefault(us => us.Login == User.Identity.Name);
            string url = Request.Url.AbsoluteUri + "/" + id;
            //System.IO.File.AppendAllText("D:\\TestFile.txt", "Телефон: " + phone + "\r\n" + url);
            MailAddress from = new MailAddress("ilya.lazarev331@gmail.com", "Ilya");
            MailAddress to = new MailAddress(user.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Объявление";
            m.Body = "Телефон: " + phone + "\n" + url;
            string s = AppDomain.CurrentDomain.BaseDirectory;
            m.Attachments.Add(new Attachment(s + car.Photo));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);            
            smtp.Credentials = new NetworkCredential("ilya.lazarev331@gmail.com", "bkmzkfpfh331");
            smtp.EnableSsl = true;
            smtp.Send(m);
            car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetItems(int id)
        {
            ViewBag.Models = db.Models.Where(c => c.BrandID == id).ToList();
            ViewBag.Bodies = db.Models.FirstOrDefault(c => c.BrandID == id).Bodies.ToList();
            return PartialView();
        }

        public ActionResult GetBody(int id)
        {
            ViewBag.Bodies = db.Models.FirstOrDefault(t => t.ID == id).Bodies.ToList();
            return PartialView();
        }
    }
}