using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourMgmtAPI.Models;

public partial class TripMaster
{
    public int TripMasterId { get; set; }

    public int BusId { get; set; }

    public int TripId { get; set; }

    public int NumberOfPassengers { get; set; }

    public DateOnly TripDate { get; set; }

    public string ConductorName { get; set; } = null!;

    public string DriverName { get; set; } = null!;

    
    public virtual Bus Bus { get; set; } = null!;
 
    public virtual Trip Trip { get; set; } = null!;
}
