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

        public IActionResult SearchData(int page, int type)
        {
            var cityList = new List<SearchLineView>();
            if (type == 0)
            {
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
                cityList = new List<SearchLineView>()
                {
                    new SearchLineView{Id=4,from_station_name="鞍山",from_train_code="K96",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K96",all_time="11时11分",change_time="无",change_times=0,from_time="00:29",to_time="11:40" },
                    new SearchLineView{Id=2,from_station_name="鞍山西",from_train_code="D52",from_train_type="动车",change_station_name="无",to_station_name="北京",to_station_code="D52",all_time="5时2分",change_time="无" ,change_times=0,from_time="10:23",to_time="15:25"},
                    new SearchLineView{Id=3,from_station_name="鞍山",from_train_code="K55",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K55",all_time="10时34分",change_time="无",change_times=0,from_time="15:50",to_time="02:24" },
                    new SearchLineView{Id=1,from_station_name="鞍山西",from_train_code="G396",from_train_type="高铁",change_station_name="无",to_station_name="北京南",to_station_code="G396",all_time="4时23分",change_time="无" ,change_times=0,from_time="18:07",to_time="22:30"},
                    new SearchLineView{Id=5,from_station_name="鞍山",from_train_code="2550",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="2550",all_time="13时12分",change_time="无" ,change_times=0,from_time="22:58",to_time="12:10"}
                };
            }else if(type==2)
            {
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.ToList();
                    cityList=Mapper.Map<List<SearchLineView>>(list);
                }

                // cityList = new List<SearchLineView>()
                // {
                //     new SearchLineView{Id=1,from_station_name="周水子国际机场",from_train_code="EU2279",change_station_name="萧山国际机场T3",to_station_name="嘉兴南",to_station_code="D3126",all_time="2时31分",change_time="1时54分",change_times=1,vehicle_type="飞机",from_time="15:35:00",to_time="18:06:00"},
                // };
            }
            
            return Json(cityList.OrderBy(k=>k.Id).ToList());
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