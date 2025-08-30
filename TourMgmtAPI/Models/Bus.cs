using System;
using System.Collections.Generic;

namespace TourMgmtAPI.Models;

public partial class Bus
{
    public int BusId { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public string FuelType { get; set; } = null!;

    public int Capacity { get; set; }

    public int ModelYear { get; set; }

    public string Manufacturer { get; set; } = null!;

    public virtual ICollection<TripMaster> TripMasters { get; set; } = new List<TripMaster>();
}
