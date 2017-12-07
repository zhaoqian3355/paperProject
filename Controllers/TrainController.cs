using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using paperProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using paperProject.ViewModels;
using AutoMapper;

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
            var trainList = new List<TrainView>();
            try
            {
                using (var db = new PaperProjectContext())
                {
                    var list = db.Train.Take(page).ToList();
                    trainList=Mapper.Map<List<TrainView>>(list);
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

        public IActionResult StationData(int page)
        {
            var cityList = new List<CityView>();
            try
            {
                using (var db = new PaperProjectContext())
                {
                    var list = db.City.Take(page).ToList();
                    cityList=Mapper.Map<List<CityView>>(list);
                    cityList.ForEach(k =>
                    {
                        var train=db.Train.FirstOrDefault(p=>p.from_station.Contains(k.CityName));
                        if(train!=null)
                            k.StationName = train.from_station;
                    });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(cityList);
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