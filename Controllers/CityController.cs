using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using paperProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace paperProject.Controllers
{
    public class CityController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new PaperProjectContext())
            {
               var cities=db.City.Take(100);
            }

            return View();
        }

        public IActionResult List(int page)
        {
            var cityList = new List<City>();
            using (var db = new PaperProjectContext())
            {
                cityList = db.City.Take(page).ToList();
            }

            return Json(cityList);
        }
    }
}