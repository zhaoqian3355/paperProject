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
                    var list = db.City.Take(100).ToList();
                    cityList=Mapper.Map<List<CityView>>(list);
                    cityList.ForEach(k =>
                    {
                        var train=db.Train.Where(p=>p.from_station.Contains(k.CityName)).OrderBy(p=>p.from_station).ToList();
                        if(train!=null){
                            var stations="";
                            train.Select(p=>p.from_station).Distinct().ToList().ForEach(p=>{
                                stations+=p+"，";
                            });
                            
                            k.StationName = stations.TrimEnd(new char[]{'，'});
                        }
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
    }
}