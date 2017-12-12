using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using System.Diagnostics;
using paperProject.Services;
using System.Collections.Generic;
using System.Linq;
using paperProject.ViewModels;
using AutoMapper;
using System;

namespace paperProject.Controllers
{
    public class FlightController : Controller
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
            var flightList = new List<Flight>();
            using (var db = new PaperProjectContext())
            {
                flightList = db.Flight.Take(page).ToList();
            }

            return Json(flightList);
        }
        public IActionResult Station()
        {
            return View();
        }

        public IActionResult StationData(int page)
        {
            var cityList = new List<CityView>();
            try
            {
                using (var db = new PaperProjectContext())
                {
                    var list = db.City.Take(100).ToList();
                    cityList=Mapper.Map<List<CityView>>(list);
                    cityList.ForEach(k =>
                    {
                        var flight=db.Flight.FirstOrDefault(p=>p.acn==k.CityName);
                        if(flight!=null)
                            k.StationName = flight.apbn;
                    });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(cityList.Where(k=>!string.IsNullOrEmpty(k.StationName)).Take(10).ToList());
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
