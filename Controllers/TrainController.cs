using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using paperProject.Services;

namespace paperProject.Controllers
{
    public class TrainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Line()
        {
            return View();
        }
        public IActionResult LineData(int page)
        {
            var trainList = new List<Train>();
            using (var db = new PaperProjectContext())
            {
                trainList = db.Train.Take(page).ToList();
            }

            return Json(trainList);
        }
        public IActionResult Station()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}