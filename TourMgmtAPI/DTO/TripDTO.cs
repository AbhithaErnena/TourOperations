using System.ComponentModel.DataAnnotations;

namespace TourMgmtAPI.DTO
{
    public class TripDTO
    {
        
        public string NameOfTrip { get; set; }
        public string StartingLocation { get; set; }
        public string DestinationLocation { get; set; }
        public int DurationOfTrip { get; set; }
        public decimal CostOfTrip { get; set; }

    }

}
