using TourMgmtAPI.Models;
using TourMgmtAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace TourMgmtAPI.Services
{
    public class BusService:IBusService
    {
        public TourMgmtDbContext context;
        public BusService(TourMgmtDbContext _context)
        {
            context = _context;
        }
        public async Task<int> AddBus(Bus bus)
        {
            int affected = 0;
            try
            {
                await context.Buses.AddAsync(bus);
                affected = await context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return affected;
        }
        public async Task<int> UpdateBus(int id, BusDTO bus)
        {
            int affected = 0;
            try
            {
                var existingBus = await context.Buses.FindAsync(id);
                if (existingBus == null) return 0;
                existingBus.RegistrationNumber = bus.RegistrationNumber;
                existingBus.FuelType = bus.FuelType;
                existingBus.Capacity = bus.Capacity;
                existingBus.ModelYear = bus.ModelYear;
                existingBus.Manufacturer = bus.Manufacturer;
                affected = await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return affected;
        }
        public async Task<int> DeleteBus(int id)
        {
            int affected = 0;
            try
            {
                var busToDelete = await context.Buses.FindAsync(id);
                if (busToDelete == null) return 0;
                context.Buses.Remove(busToDelete);
                affected = await context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                return -1;
            }
            return affected;    
        }
        public async Task<List<Bus>> GetBuses()
        {
            try
            {
                return await context.Buses.ToListAsync();
            }
            catch(Exception)
            {
                return new List<Bus>();
            }
        }
        public async Task<Bus> FindBusById(int id)
        {
            Bus busById = null;
            try
            {
                busById =await context.Buses.FindAsync(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return busById;
        }
        public async Task<List<Bus>> FindBusByName(string name)
        {
            List<Bus> busByName = null;
            try
            {
                busByName = await context.Buses.
                            Where(b=>b.RegistrationNumber.ToLower().Contains(name.ToLower()) ||
                                     b.Manufacturer.ToLower().Contains(name.ToLower()))
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return busByName;
        }
    }
}
