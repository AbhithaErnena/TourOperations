using TourMgmtAPI.Models;
using TourMgmtAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace TourMgmtAPI.Services
{
    public class TripService:ITripService
    {
        TourMgmtDbContext context;
        public TripService(TourMgmtDbContext _context)
        {
            context = _context;
        }

        public async Task<int> AddTrip(Trip trip)
        {
            int affected = 0;
            try
            {
                await context.Trips.AddAsync(trip);
                affected =await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return affected;
        }
       public async Task<List<Trip>> GetTrips()
        {
            List<Trip> allTrips= new List<Trip>();
            try
            {
                allTrips =await context.Trips.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return allTrips;
        }
        public async Task<Trip> FindTripById(int id)
        {
            Trip trip = null;
            try
            {
                trip =await context.Trips.FindAsync(id);
            }catch(Exception ex)
            {
                throw ex;
            }
            return trip;
        }
        public async Task<Trip> FindTripByName(string name)
        {
            Trip tripByName= null;
            try
            {
                tripByName =await context.Trips.FirstOrDefaultAsync(t => t.NameOfTrip.ToLower().Contains(name.ToLower()));
            }catch( Exception ex)
            {
                throw ex ;
            }
            return tripByName;
        }
        public async Task<int> UpdateTrip(int id, TripDTO trip)
        {
            int affected = 0;
            try
            {
                var existingTrip = await context.Trips.FirstOrDefaultAsync(t => t.TripId == id);
                if (existingTrip == null) return 0;
                existingTrip.NameOfTrip = trip.NameOfTrip;
                existingTrip.StartingLocation = trip.StartingLocation;
                existingTrip.DestinationLocation = trip.DestinationLocation;
                existingTrip.DurationOfTrip = trip.DurationOfTrip;
                existingTrip.CostOfTrip = trip.CostOfTrip;
                affected =await context.SaveChangesAsync();

            }catch(Exception ex)
            {
                throw ex;
            }
            return affected;
        }
        public async Task<int> DeleteTrip(int id)
        {
            int affected = 0;
            try
            {
                var tripToDelete =await context.Trips.FirstOrDefaultAsync(t => t.TripId == id);
                if (tripToDelete == null) return 0;
                context.Trips.Remove(tripToDelete);
                affected = await context.SaveChangesAsync();
            }catch (DbUpdateException ex)
            {
                return -1;
            }
            return affected;
        }

    }
}
