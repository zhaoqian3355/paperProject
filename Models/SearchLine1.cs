namespace paperProject.Models
{
    public class SearchLine1
    {
         public int Id{get;set;}
        public string from_station_name{get;set;}
        public string from_train_code{get;set;}
        public string change_station_name{get;set;}
        public string change_train_code{get;set;}
        public string to_station_name{get;set;}
        public string all_time { get; set; }
        public string change_time { get; set; }
        public string price { get; set; }
        public int all_minutes { get; set; }
        public int change_minutes { get; set; }
    }
}