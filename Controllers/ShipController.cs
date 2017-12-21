using Microsoft.AspNetCore.Mvc;
using paperProject.Models;
using System.Diagnostics;
using System.Collections.Generic;
using paperProject.Services;
using AutoMapper;
using paperProject.ViewModels;
using System;
using System.Linq;

namespace paperProject.Controllers
{
    public class ShipController : Controller
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
            var shipList = new List<Ship>();
            using (var db = new PaperProjectContext())
            {
                shipList = db.Ship.Take(page).ToList();
            }

            return Json(shipList);
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
                    var list = db.Ship.ToList();
                    var cities = db.City.ToList();
                    cityList=Mapper.Map<List<CityView>>(cities);
                    list.ForEach(k =>
                    {
                        var city= cityList.FirstOrDefault(p=>p.CityName.Contains(k.from_city));
                        if(city != null)
                            city.StationName = k.from_dock_name;
                    });
                }
            }
            catch (Exception ex)
            {

            }

            return Json(cityList.Where(k=>!string.IsNullOrEmpty(k.StationName)).ToList());
        }
    }
}