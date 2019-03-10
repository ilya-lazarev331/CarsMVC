using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        UserContext db = new UserContext();

        public ViewResult Index()
        {
            //SelectList cars = new SelectList(db.Cars, "Brand", "Brand");            
            string result = "Вы не авторизованы";
            int number = db.Cars.Count();
            if (User.Identity.IsAuthenticated)
            {               
                result = "Ваш логин: " + User.Identity.Name;
            }
            ViewBag.Number = number;
            ViewBag.Result = result;
            var cars = db.Cars.OrderByDescending(p => p.Date).ToList();
            //IEnumerable<Car> cars = db.Cars.ToList();            
            return View(cars); //return View(db.Cars);
        }

        public ActionResult Mail()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Mail(EmailModels model)
        {
            string To, From, Subject, Body, Password;
            if (ModelState.IsValid)
            {
                To = model.To;
                From = model.From;
                Subject = model.Subject;
                Body = model.Body;
                Password = model.Password;
                try
                {
                    Send(To, From, Subject, Body, Password);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Error");
                }
            }
            return View(model);
        }

        public static void Send(string To, string From, string Subject, string Body, string Password)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;            
            client.Credentials = new NetworkCredential(From, Password);
            MailMessage msg = new MailMessage(From, To, Subject, Body);
            client.Send(msg);
        }             

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Filter()
        {              
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.Models = null;
            ViewBag.Bodies = db.Bodies.ToList();
            ViewBag.Cities = db.Cities.ToList();
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult Post()
        {
            User user = db.Users.FirstOrDefault(us => us.Login == User.Identity.Name);
            var cars = db.Cars.OrderByDescending(p => p.Date).Where(m => m.UserID == user.ID).ToList();                       
            return View(cars);
        }

        [HttpPost]
        public ActionResult FilterList(int BrandID, int ModelID, int BodyID, int CityID, int? Price1, int? Price2, int Sort)
        {
            IEnumerable<Car> cars = db.Cars.ToList();
            if (BrandID != 1010) cars = cars.Where(m => m.BrandID == BrandID);
            if (ModelID != 1010) cars = cars.Where(m => m.ModelID == ModelID);
            if (BodyID != 1010) cars = cars.Where(m => m.BodyID == BodyID);
            if (CityID != 1010) cars = cars.Where(m => m.CityID == CityID);
            if (Price1 != null)
            {
                if (Price2 != null) cars = cars.Where(m => m.Price >= Price1 && m.Price <= Price2);
                else cars = cars.Where(m => m.Price >= Price1);
            }
            else if(Price2 != null) cars = cars.Where(m => m.Price <= Price2);
            if (Sort == 1) cars = cars.OrderByDescending(p => p.Date).ToList();
            if (Sort == 2) cars = cars.OrderByDescending(p => p.Price).ToList();
            if (Sort == 3) cars = cars.OrderBy(p => p.Price).ToList();
            if (Sort == 4) cars = cars.OrderByDescending(p => p.Year).ToList();
            return PartialView(cars);
        }

        public ActionResult GetItems2(int id)
        {
            if (id == 1010)
            {
                ViewBag.Models = null;
                ViewBag.Bodies = db.Bodies.ToList();
            }
            else
            {
                ViewBag.Models = db.Models.Where(c => c.BrandID == id).ToList();
                ViewBag.Bodies = db.Models.FirstOrDefault(c => c.BrandID == id).Bodies.ToList();
            }
            return PartialView();
        }

        public ActionResult GetBody2(int id)
        {
            if (id == 1010)
            {
                ViewBag.Bodies = db.Bodies.ToList();
            }
            else
            {
                ViewBag.Bodies = db.Models.FirstOrDefault(t => t.ID == id).Bodies.ToList();
            }
            return PartialView();
        }
    }
}