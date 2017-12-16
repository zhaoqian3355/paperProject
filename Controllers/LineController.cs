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
    public class LineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult SearchData(int page, int type)
        {
            var cityList = new List<SearchLineView>()
            {
                new SearchLineView{Id=1,from_station_name="大连",from_train_code="G1251",change_station_name="上海虹桥",to_station_name="嘉兴南",to_station_code="G7383",all_time="12时28分",change_time="1时8分",change_times=1 },
                new SearchLineView{Id=2,from_station_name="大连",from_train_code="G8001",change_station_name="鞍山西",to_station_name="嘉兴南",to_station_code="G1226",all_time="13时16分",change_time="56分" ,change_times=1},
                new SearchLineView{Id=3,from_station_name="大连北",from_train_code="G8041",change_station_name="上海虹桥",to_station_name="嘉兴南",to_station_code="G7383",all_time="13时21分",change_time="1时22分",change_times=1 },
                new SearchLineView{Id=4,from_station_name="大连",from_train_code="G1251",change_station_name="南京南",to_station_name="嘉兴",to_station_code="K1805",all_time="20时27分",change_time="5时2分" ,change_times=1}
            };
            
            return Json(cityList);
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
                    cityList = Mapper.Map<List<CityView>>(list);
                    cityList.ForEach(k =>
                    {
                        var train = db.Train.Where(p => p.from_station.Contains(k.CityName)).OrderBy(p => p.from_station).ToList();
                        if (train != null)
                        {
                            var stations = "";
                            train.Select(p => p.from_station).Distinct().ToList().ForEach(p =>
                            {
                                stations += p + "，";
                            });

                            k.StationName = stations.TrimEnd(new char[] { '，' });
                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(cityList.Where(k => !string.IsNullOrEmpty(k.StationName)).Take(10).ToList());
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