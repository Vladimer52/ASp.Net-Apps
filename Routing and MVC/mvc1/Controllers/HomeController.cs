using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc1.Models;

namespace mvc1.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewData["data"] = "Hello ASP.NET";
            ViewBag.Message = "Hello ViewBag";
            ViewBag.Countries = new List<string> { "Бразилия", "Австралия", "Россиия" };
            return View(db.Phones.ToList());
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.PhoneId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return "Thanks " + order.User + " for sale";
        }

        [NonAction] //метод не используется как действие
        public string Hello() => "Hello Aps.NET";

        [ActionName("Welcome")] //изменяет имя действия
        public string Hello2( int id = 1) => $"id = {id}";
    }
}
