using TourMgmtAPI.Models;
using TourMgmtAPI.DTO;
namespace TourMgmtAPI.Services
{
    public interface IBusService
    {
        Task<int> AddBus(Bus bus);
        Task<int> UpdateBus(int id, BusDTO bus);
        Task<int> DeleteBus(int id);
        Task<List<Bus>> GetBuses();
        Task<Bus> FindBusById(int id);
        Task<List<Bus>> FindBusByName(string name);
    }
}
