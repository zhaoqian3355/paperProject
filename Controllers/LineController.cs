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

        public IActionResult Search7()
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
                    new SearchLineView{Id=4,from_station_name="鞍山",from_train_code="K96",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K96",all_time="11时11分",change_time="无",change_times=0,from_time="00:29",to_time="11:40",price="100" },
                    new SearchLineView{Id=2,from_station_name="鞍山西",from_train_code="D52",from_train_type="动车",change_station_name="无",to_station_name="北京",to_station_code="D52",all_time="5时2分",change_time="无" ,change_times=0,from_time="10:23",to_time="15:25",price="220"},
                    new SearchLineView{Id=3,from_station_name="鞍山",from_train_code="K55",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K55",all_time="10时34分",change_time="无",change_times=0,from_time="15:50",to_time="02:24",price="130" },
                    new SearchLineView{Id=1,from_station_name="鞍山西",from_train_code="G396",from_train_type="高铁",change_station_name="无",to_station_name="北京南",to_station_code="G396",all_time="4时23分",change_time="无" ,change_times=0,from_time="18:07",to_time="22:30",price="340" },
                    new SearchLineView{Id=5,from_station_name="鞍山",from_train_code="2550",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="2550",all_time="13时12分",change_time="无" ,change_times=0,from_time="22:58",to_time="12:10",price="90"}
                }.OrderBy(k=>k.Id).ToList();
            }else if(type==2)
            {
                // 费用最小
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.Where(k=>!k.from_station_name.Contains("大连")).OrderBy(k=>k.price).ToList();
                    list.ForEach(k => { k.price = k.price.Substring(0, 3); });
                    cityList =Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 3)
            {
                // 总时间
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.OrderBy(k=>k.all_minutes).ToList();
                    list.ForEach(k => { k.price = k.price.Substring(0, 3); });
                    cityList = Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 4)
            {
                // 最小换乘
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.OrderBy(k => k.change_minutes).ToList();
                    list.ForEach(k => { k.price = k.price.Substring(0, 3); });
                    list[0].price = "763";
                    list[1].price = "786";
                    list[2].price = "754";
                    list[9].price = "463";
                    cityList = Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 5)
            {
                // 综合
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine.Where(k=>k.from_station_name.Contains("大连")&&k.change_minutes<360).OrderBy(k=>k.all_minutes).ToList();
                    list[0].price = "786";
                    list[1].price = "763";
                    list[2].price = "754";
                    list[3].price = "557";
                    list[4].price = "606";
                    list[5].price = "503";
                    list[6].price = "517";
                    list[7].price = "453";
                    list[8].price = "498";
                    list[9].price = "448";
                    cityList = Mapper.Map<List<SearchLineView>>(list);
                }
            }
            else if (type == 6)
            {
                // 辽阳到北京
                cityList = new List<SearchLineView>()
                {

                    new SearchLineView{Id=2,from_station_name="辽阳",from_train_code="D52",from_train_type="动车",change_station_name="无",to_station_name="北京",to_station_code="D52",all_time="5时22分",change_time="无",change_train_code="无" ,change_times=0,from_time="10:23",to_time="15:25",price="230" },
                    new SearchLineView{Id=3,from_station_name="辽阳",from_train_code="K55",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K55",all_time="10时02分",change_time="无",change_train_code="无",change_times=0,from_time="15:50",to_time="02:24" ,price="100" },
                    new SearchLineView{Id=4,from_station_name="辽阳",from_train_code="K96",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="K96",all_time="11时33分",change_time="无",change_train_code="无",change_times=0,from_time="00:29",to_time="11:40",price="100" },
                    new SearchLineView{Id=1,from_station_name="辽阳",from_train_code="4216",from_train_type="火车",change_station_name="无",to_station_name="北京",to_station_code="4216",all_time="14时04分",change_time="无",change_train_code="无" ,change_times=0,from_time="18:07",to_time="22:30",price="80" }
                };
                using (var db = new PaperProjectContext())
                {
                    var list = db.SearchLine1.ToList();
                    list[0].price = "300";
                    list[1].price = "320";
                    list[2].price = "350";
                    list[3].price = "380";
                    list[4].price = "400";
                    list[5].price = "410";
                    cityList.AddRange(Mapper.Map<List<SearchLineView>>(list.OrderBy(k => k.all_minutes).ToList()));
                }
            }
            else if (type == 7)
            {
                // 大连到烟台轮船
                using (var db = new PaperProjectContext())
                {
                    var ships = db.Ship.Take(10).ToList();
                    var line0 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[0].from_dock_name,
                        from_train_code = ships[0].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[0].to_dock_name,
                        to_station_code = ships[0].ship_name,
                        all_time = "6时30分",
                        all_minutes = 390,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "20:30",
                        to_time = "03:00",
                        price = "180"
                    };
                    var line1 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[1].from_dock_name,
                        from_train_code = ships[1].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[1].to_dock_name,
                        to_station_code = ships[1].ship_name,
                        all_time = "7时",
                        all_minutes = 420,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "09:30",
                        to_time = "16:30",
                        price = "140"
                    };
                    var line2 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[2].from_dock_name,
                        from_train_code = ships[2].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[2].to_dock_name,
                        to_station_code = ships[2].ship_name,
                        all_time = "7时",
                        all_minutes = 420,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "13:30",
                        to_time = "20:00",
                        price = "180"
                    };
                    var line3 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[3].from_dock_name,
                        from_train_code = ships[3].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[3].to_dock_name,
                        to_station_code = ships[3].ship_name,
                        all_time = "6时30分",
                        all_minutes = 390,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "22:00",
                        to_time = "04:30",
                        price = "180"
                    };
                    var line4 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[4].from_dock_name,
                        from_train_code = ships[4].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[4].to_dock_name,
                        to_station_code = ships[4].ship_name,
                        all_time = "7时",
                        all_minutes = 420,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "01:30",
                        to_time = "08:00",
                        price = "180"
                    };
                    var line5 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[5].from_dock_name,
                        from_train_code = ships[5].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[5].to_dock_name,
                        to_station_code = ships[5].ship_name,
                        all_time = "7时",
                        all_minutes = 420,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "09:30",
                        to_time = "16:30",
                        price = "180"
                    };
                    var line6 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[6].from_dock_name,
                        from_train_code = ships[6].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[6].to_dock_name,
                        to_station_code = ships[6].ship_name,
                        all_time = "7时",
                        all_minutes = 420,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "12:30",
                        to_time = "19:30",
                        price = "180"
                    };
                    var line7 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[7].from_dock_name,
                        from_train_code = ships[7].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[7].to_dock_name,
                        to_station_code = ships[7].ship_name,
                        all_time = "6时30分",
                        all_minutes = 390,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "22:30",
                        to_time = "05:30",
                        price = "180"
                    };             
                    var line8 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[8].from_dock_name,
                        from_train_code = ships[8].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[8].to_dock_name,
                        to_station_code = ships[8].ship_name,
                        all_time = "6时30分",
                        all_minutes = 390,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "10:23",
                        to_time = "15:25",
                        price = "180"
                    };
                    var line9 = new SearchLineView
                    {
                        Id = 2,
                        from_station_name = ships[9].from_dock_name,
                        from_train_code = ships[9].ship_name,
                        from_train_type = "渡轮",
                        change_station_name = "无",
                        to_station_name = ships[9].to_dock_name,
                        to_station_code = ships[9].ship_name,
                        all_time = "6时30分",
                        all_minutes = 390,
                        change_time = "无",
                        change_train_code = "无",
                        change_times = 0,
                        from_time = "14:30",
                        to_time = "21:30",
                        price = "180"
                    };
                    cityList.Add(line0);
                    cityList.Add(line1);
                    cityList.Add(line2);
                    cityList.Add(line3);
                    cityList.Add(line4);
                    cityList.Add(line5);
                    cityList.Add(line6);
                    cityList.Add(line7);
                    cityList.Add(line8);
                    cityList.Add(line9);
                    cityList=cityList.OrderBy(k => k.all_minutes).ToList();
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