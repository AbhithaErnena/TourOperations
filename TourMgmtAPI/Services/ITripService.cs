using TourMgmtAPI.Models;
using TourMgmtAPI.DTO;

namespace TourMgmtAPI.Services
{
    public interface ITripService
    {
        Task<int> AddTrip(Trip trip);
        Task<List<Trip>> GetTrips();
        Task<Trip> FindTripById(int id);
        Task<Trip> FindTripByName(string name);
        Task<int> UpdateTrip(int id,TripDTO trip);
        Task<int> DeleteTrip(int id);
    }
}
