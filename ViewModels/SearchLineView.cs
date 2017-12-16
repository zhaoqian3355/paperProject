namespace paperProject.ViewModels
{
    public class SearchLineView
    {
        public int Id{get;set;}
        public string from_station_name{get;set;}
        public string from_train_code{get;set;}
        public string change_station_name{get;set;}
        public string change_train_code{get;set;}
        public string to_station_name{get;set;}
        public string to_station_code { get; set; }
        public string all_time { get; set; }
        public string change_time { get; set; }
        public int change_times { get; set; }
    }
}