namespace paperProject.ViewModels
{
    public class CityView
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string StationName{get;set;}
    }
}