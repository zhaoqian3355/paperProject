using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using paperProject.Services;
using paperProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace paperProject.Controllers
{
    public class LineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search0()
        {
            return View();
        }

        public IActionResult Search1()
        {
            return View();
        }

        public IActionResult Search2()
        {
            return View();
        }

        public IActionResult Search3()
        {
            return View();
        }

        public IActionResult Search4()
        {
            return View();
        }

        public IActionResult Search5()
        {
            return View();
        }
        public IActionResult Search6()
        {
            return View();
        }

        public IActionResult SearchData(int page, int type)
        {
            var cityList = new List<SearchLineView>();
            if (type == 0)
            {
                // 火车换乘
                cityList = new List<SearchLineView>()
                {
                    new SearchLineView{Id=1,from_station_name="大连",from_train_code="G1251",change_station_name="上海虹桥",to_station_name="嘉兴南",to_station_code="G7383",all_time="12时28分",change_time="1时8分",change_times=1 },
                    new SearchLineView{Id=2,from_station_name="大连",from_train_code="G8001",change_station_name="鞍山西",to_station_name="嘉兴南",to_station_code="G1226",all_time="13时16分",change_time="56分" ,change_times=1},
                    new SearchLineView{Id=3,from_station_name="大连北",from_train_code="G8041",change_station_name="上海虹桥",to_station_name="嘉兴南",to_station_code="G7383",all_time="13时21分",change_time="1时22分",change_times=1 },
                    new SearchLineView{Id=4,from_station_name="大连",from_train_code="G1251",change_station_name="南京南",to_station_name="嘉兴",to_station_code="K1805",all_time="20时27分",change_time="5时2分" ,change_times=1}
                };
            }
            else if(type==1)
            {
                // 火车直达
                cityList = new List<SearchLineView>()
                {
                    new SearchLineView{Id=4,from_station_name="鞍山",from_train_code="K96",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K96",all_time="11时11分",change_time="无",change_times=0,from_time="00:29",to_time="11:40" },
                    new SearchLineView{Id=2,from_station_name="鞍山西",from_train_code="D52",from_train_type="动车",change_station_name="无",to_station_name="北京",to_station_code="D52",all_time="5时2分",change_time="无" ,change_times=0,from_time="10:23",to_time="15:25"},
                    new SearchLineView{Id=3,from_station_name="鞍山",from_train_code="K55",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K55",all_time="10时34分",change_time="无",change_times=0,from_time="15:50",to_time="02:24" },
                    new SearchLineView{Id=1,from_station_name="鞍山西",from_train_code="G396",from_train_type="高铁",change_station_name="无",to_station_name="北京南",to_station_code="G396",all_time="4时23分",change_time="无" ,change_times=0,from_time="18:07",to_time="22:30"},
                    new SearchLineView{Id=5,from_station_name="鞍山",from_train_code="2550",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="2550",all_time="13时12分",change_time="无" ,change_times=0,from_time="22:58",to_time="12:10"}
                }.OrderBy(k=>k.Id).ToList();
            }else if(type==2)
            {
                // 费用最小
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.Where(k=>!k.from_station_name.Contains("大连")).OrderBy(k=>k.price).ToList();
                    cityList=Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 3)
            {
                // 总时间
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.OrderBy(k=>k.all_minutes).ToList();
                    cityList = Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 4)
            {
                // 最小换乘
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.OrderBy(k => k.change_minutes).ToList();
                    cityList = Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 5)
            {
                // 综合
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.Where(k=>k.from_station_name.Contains("大连")).ToList();
                    cityList = Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 6)
            {
                // 综合
                cityList = new List<SearchLineView>()
                {
                    new SearchLineView{Id=4,from_station_name="辽阳",from_train_code="K96",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K96",all_time="11时33分",change_time="无",change_train_code="无",change_times=0,from_time="00:29",to_time="11:40" },
                    new SearchLineView{Id=2,from_station_name="辽阳",from_train_code="D52",from_train_type="动车",change_station_name="无",to_station_name="北京",to_station_code="D52",all_time="5时22分",change_time="无",change_train_code="无" ,change_times=0,from_time="10:23",to_time="15:25"},
                    new SearchLineView{Id=3,from_station_name="辽阳",from_train_code="K55",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K55",all_time="10时02分",change_time="无",change_train_code="无",change_times=0,from_time="15:50",to_time="02:24" },
                    new SearchLineView{Id=1,from_station_name="辽阳",from_train_code="4216",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="4216",all_time="14时04分",change_time="无",change_train_code="无" ,change_times=0,from_time="18:07",to_time="22:30"}
                };
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine1.ToList();
                    cityList.AddRange(Mapper.Map<List<SearchLineView>>(list));
                }
            }

            return Json(cityList.Take(10).ToList());
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