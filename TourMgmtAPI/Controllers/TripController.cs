using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMgmtAPI.Models;
using TourMgmtAPI.Services;
using TourMgmtAPI.DTO;
using Microsoft.AspNetCore.Authorization;

namespace TourMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        public ITripService tripService;
        public TripController(TourMgmtDbContext context)
        {
            tripService=new TripService(context);
        }

        //get all trips
        //api/Trip
        [Authorize(Roles = "admin,User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAllTrips()
        {
            var trips=await tripService.GetTrips();
            if(trips==null || !trips.Any())
            {
                return Ok(new { message = "No Trips data found" });
            }
            return Ok(trips);
        }

        //get trip by id
        //api/Trip/{id}
        [Authorize(Roles = "admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTripById(int id)
        {
            Trip trip=await tripService.FindTripById(id);
            if(trip==null)
            {
                return NotFound(new { message = $"Trip with ID {id} not found" });
            }
            return Ok(trip);
        }

        //get trip by name
        //api/Trip/search/{name}
        [Authorize(Roles = "admin,User")]
        [HttpGet("search/{name}")]
        public async Task<ActionResult<Trip>> GetTripByName(string name)
        {
            Trip trip= await tripService.FindTripByName(name);
            if(trip==null)
            {
                return NotFound(new { message = $"Trip with name {name} not found." });
            }
            return Ok(trip);
        }

        //add a trip //to avoid model binding errors
        //api/Trip/create
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<ActionResult> AddTrip([FromBody] TripDTO tripdto) 
        {
            Trip trip = new Trip()
            {
                NameOfTrip = tripdto.NameOfTrip,
                StartingLocation=tripdto.StartingLocation,
                DestinationLocation=tripdto.DestinationLocation,
                DurationOfTrip=tripdto.DurationOfTrip,
                CostOfTrip=tripdto.CostOfTrip
            };
            var result=await tripService.AddTrip(trip);
            if(result>0)
            {
                return Ok(new { message = "Trip Added Successfully." });
            }
            return BadRequest(new { message = "Failed to add Trip." });
        }

        //update trip
        //api/Trip/update/{id}
        [Authorize(Roles = "admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateTrip(int id,[FromBody] TripDTO trip)
        {
            var result=await tripService.UpdateTrip(id,trip);
            if(result>0)
            {
                return Ok(new { message = "Trip updated successfully" });
            }
            return NotFound(new { message = $"Trip with ID {id} not found or update failed." });
        }

        //delete trip
        //api/Trip/delete/{id}
        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTrip(int id)
        {
            var result= await tripService.DeleteTrip(id);
            if(result>0)
            {
                return Ok(new { message = "Trip deleted successfully." });
            }
            if(result==-1)
            {
                return Ok(new { message = "Cannot delete trip. It is assigned to one or more scheduled journeys." });
            }
            return NotFound(new { message = $"Trip with ID {id} not found or delete failed" });
        }
    }
}
