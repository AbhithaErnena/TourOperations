using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMgmtAPI.DTO;
using TourMgmtAPI.Models;
using TourMgmtAPI.Services;

namespace TourMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripMasterController : ControllerBase
    {
        public ITripMasterService tripMasterService;
        public TripMasterController(TourMgmtDbContext context)
        {
            tripMasterService=new TripMasterService(context);
        }

        //get all TripMaster
        //api/TripMaster
        [Authorize(Roles = "admin,User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripMaster>>> GetAllTripMaster() 
        {
            var tripMasters =await  tripMasterService.GetTM();
            if(tripMasters==null ||!tripMasters.Any())
            {
                return Ok(new { message = "No Trip master data found." });
            }
            return Ok(tripMasters);
        }

        //get trip master by id
        //api/TripMaster/{id}
        [Authorize(Roles = "admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TripMaster>> GetTripMasterById(int id)
        {
            var tripMaster=await tripMasterService.FindTMById(id);
            if(tripMaster==null)
            {
                return NotFound(new { message = $"TripMaster with ID {id} not found." });
            }
            return Ok(tripMaster);
        }

        //get trip master by name
        //api/TripMaster/search/{id}
        [Authorize(Roles = "admin,User")]
        [HttpGet("search/{name}")]
        public async Task<ActionResult<TripMaster>> GetTripMasterByName(string name)
        {
            var tripMaster = await tripMasterService.FindTMName(name);
            if(tripMaster == null)
            {
                return NotFound(new { message = $"TripMaster with name {name} not found" });  
            }
            return Ok(tripMaster);
        }

        //add trip master by name
        //api/TripMaster/create
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<ActionResult> AddTripMaster([FromBody] TripMasterDTO dto)
        {
            var tripMaster = new TripMaster()
            {
                BusId = dto.BusId,
                TripId = dto.TripId,
                NumberOfPassengers = dto.NumberOfPassengers,
                TripDate = dto.TripDate,
                ConductorName = dto.ConductorName,
                DriverName = dto.DriverName
            };
            var result=await tripMasterService.AddTM(tripMaster);
            if(result>0)
            {
                return Ok(new { message = "TripMaster added successfully" });
            }
            return BadRequest("Failed to add Trip master.");
        }


        //update trip master
        //api/TripMaster/upadte
        [Authorize(Roles = "admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateTripMaster(int id,TripMasterDTO tm)
        {
            int result = await tripMasterService.UpdateTM(id, tm);
            if(result>0)
            {
                return Ok(new { message = $"Trip Master updated successfully." });
            }
            return NotFound(new { message = $"TripMaster with ID {id} not found or update failed." });
        }

        //delete trip master
        //api/TripMaster/delete/{id}
        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTripMaster(int id)
        {
            var result= await tripMasterService.DeleteTM(id);
            if(result>0)
            {
                return Ok(new { message = "Trip Master deleted successfully." });
            }
            return NotFound(new { message = $"TripMaster with ID {id} not found or delete failed." });
        }
    }
}
