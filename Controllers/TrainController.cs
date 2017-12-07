using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using paperProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                using (var db = new PaperProjectContext())
                {
                    trainList = db.Train.Take(page).ToList();
                    trainList.ForEach(k =>
                    {
                        k.TrainStations = db.TrainStation.Where(p => k.train_code == p.station_train_code).OrderBy(p => p.station_no).ToList();
                    });
                }
            }
            catch (Exception ex)
            {

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
    }
}