using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using paperProject.Services;
using System;

namespace paperProject.Controllers
{
    public class CityController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new PaperProjectContext())
            {
            }

            return View();
        }
    }
}