using System;
using System.Collections.Generic;

namespace TourMgmtAPI.Models;

public partial class Trip
{
    public int TripId { get; set; }

    public string NameOfTrip { get; set; } = null!;

    public string StartingLocation { get; set; } = null!;

    public string DestinationLocation { get; set; } = null!;

    public int DurationOfTrip { get; set; }

    public decimal CostOfTrip { get; set; }

    public virtual ICollection<TripMaster> TripMasters { get; set; } = new List<TripMaster>();
}
