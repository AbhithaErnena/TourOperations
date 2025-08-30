using TourMgmtAPI.Models;
using TourMgmtAPI.DTO;
using Microsoft.EntityFrameworkCore;
namespace TourMgmtAPI.Services
{
    public class TripMasterService:ITripMasterService
    {
        TourMgmtDbContext context;
       public TripMasterService(TourMgmtDbContext _context) 
        {
            context = _context;
        }
        public async Task<int> AddTM(TripMaster tripMaster)
        {
            int affected = 0;
            try
            {
                await context.TripMasters.AddAsync(tripMaster);
                affected = await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return affected;
        }
        public async Task<List<TripMaster>> GetTM()
        {
            List<TripMaster> allTripMaster = new List<TripMaster>();
            try
            {
                allTripMaster = await context.TripMasters.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return allTripMaster;
        }
        public async Task<TripMaster> FindTMById(int id)
        {
            TripMaster tripMaster = null;
            try
            {
                tripMaster = await context.TripMasters.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tripMaster;
        }
        public async Task<TripMaster> FindTMName(string name)
        {
            TripMaster tripMasterByName = null;
            try
            {
                tripMasterByName = await context.TripMasters.FirstOrDefaultAsync(tm => tm.ConductorName.ToLower().Contains(name.ToLower()) ||
                                                                    tm.DriverName.Contains(name.ToLower()));

            }catch(Exception ex)
            {
                throw ex;
            }
            return tripMasterByName;
        }
        public async Task<int> UpdateTM(int id, TripMasterDTO tmdto)
        {
            int affected = 0;
            try
            {
                var existingTripMaster = await context.TripMasters.FirstOrDefaultAsync(tm => tm.TripMasterId == id);
                if (existingTripMaster == null) return 0;
                existingTripMaster.BusId= tmdto.BusId;
                existingTripMaster.TripId= tmdto.TripId;
                existingTripMaster.NumberOfPassengers=tmdto.NumberOfPassengers;
                existingTripMaster.TripDate= tmdto.TripDate;
                existingTripMaster.DriverName= tmdto.DriverName;
                existingTripMaster.ConductorName= tmdto.ConductorName;
                affected = await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return affected;
        }
        public async Task<int> DeleteTM(int id)
        {
            int affected = 0;
            try
            {
                var tripMasterToDelete = await context.TripMasters.FirstOrDefaultAsync(tm => tm.TripMasterId == id);
                if (tripMasterToDelete == null) return 0;
                context.TripMasters.Remove(tripMasterToDelete);
                affected = await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return affected;
        }
    
    }
}
