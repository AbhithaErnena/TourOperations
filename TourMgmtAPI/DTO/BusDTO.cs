using System.ComponentModel.DataAnnotations;

namespace TourMgmtAPI.DTO
{
    public class BusDTO
    {
         public string RegistrationNumber { get; set; }
        public string FuelType { get; set; }
        public int Capacity { get; set; }
        public int ModelYear { get; set; }
        public string Manufacturer { get; set; }
    }

}
