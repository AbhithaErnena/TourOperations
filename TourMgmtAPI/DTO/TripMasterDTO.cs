namespace TourMgmtAPI.DTO
{
    public class TripMasterDTO
    {
        public int BusId { get; set; }
        public int TripId {  get; set; }
        public int NumberOfPassengers { get; set; }
        public DateOnly TripDate { get; set; }
        public string ConductorName {  get; set; }
        public string DriverName {  get; set; }
    }
}
