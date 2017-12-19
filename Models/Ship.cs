using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paperProject.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public string ship_name { get; set; }
        public string from_dock_name { get; set; }
        public string from_time { get; set; }
        public string to_dock_name { get; set; }
        public string ship_line { get; set; }

        public string from_city{get;set;}

        public string to_city{get;set;}

    }
}
