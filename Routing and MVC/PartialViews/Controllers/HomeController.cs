using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartialViews.Models;

namespace PartialViews.Controllers
{
    public class HomeController : Controller
    {
        List<Phone> phones;
        List<Company> companies;
        public HomeController()
        {
            Company apple = new Company { Id = 1, Country = "USA", Name = "Apple" };
            Company microsoft = new Company { Id = 2, Country = "USA", Name = "Samsung" };
            Company nokia = new Company { Id = 3, Country = "Finland", Name = "Nokia" };
            companies = new List<Company> { apple, microsoft, nokia };

            phones = new List<Phone>
            {
                new Phone { Id=1, Manufacturer= apple, Name="iPhone X", Price=56000 },
                new Phone { Id=2, Manufacturer= apple, Name="iPhone XZ", Price=41000 },
                new Phone { Id=3, Manufacturer= microsoft, Name="Galaxy 9", Price=9000 },
                new Phone { Id=4, Manufacturer= microsoft, Name="Galaxy 10", Price=40000 },
                new Phone { Id=5, Manufacturer= nokia, Name="Pixel 2", Price=30000 },
                new Phone { Id=6, Manufacturer= nokia, Name="Pixel XL", Price=50000 }
            };
        }
        public IActionResult Index() => View();
        public IActionResult Phones(int? companyId)
        {
            List<CompanyModel> companyModels = companies
                .Select(c => new CompanyModel { Id = c.Id, Name = c.Name })
                .ToList();
            companyModels.Insert(0, new CompanyModel { Id = 0, Name = "All" });
            IndexViewModel ivm = new IndexViewModel { Companies = companyModels, Phones = phones };
            // если передан id компании, фильтруем список
            if (companyId != null && companyId > 0)
                ivm.Phones = phones.Where(p => p.Manufacturer.Id == companyId);
            return View(ivm);
        }
        public IActionResult GetMessage() => PartialView("_GetMessage");

        public IActionResult GetArray() => View();

        [HttpPost]
        public IActionResult GetArray(string[] array)
        {
            string result = "";
            foreach (var st in array)
            {
                result = st + ";";
            }
            return Content(result);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string login, string pass)
        {
            string authData = $"Logint: {login}, Pass: {pass}";
            return Content(authData);
        }

        public ActionResult AddUsesr(User user)
        {
            if (ModelState.IsValid)
            {
                string UserInfo = $"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}, HasRight: {user.HasRight}";
                return Content(UserInfo);
            }
            return Content($"Количество ошибок: {ModelState.ErrorCount}");
        }
    }
}
