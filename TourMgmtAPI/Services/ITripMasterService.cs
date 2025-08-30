using TourMgmtAPI.Models;
using TourMgmtAPI.DTO;
using System.Threading.Tasks;
namespace TourMgmtAPI.Services
{
    public interface ITripMasterService
    {
        Task<int> AddTM(TripMaster tripMaster);
        Task<List<TripMaster>> GetTM();
        Task<TripMaster> FindTMById(int id);
        Task<TripMaster> FindTMName(string name);
        Task<int> UpdateTM(int id,TripMasterDTO tmdto);
        Task<int> DeleteTM(int id);
    }
}
