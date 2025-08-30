using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourMgmtAPI.Models;
using TourMgmtAPI.Services;
using TourMgmtAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using TourMgmtAPI.Accounts;

namespace TourMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        public IBusService busService;
        public BusController(TourMgmtDbContext context)
        {
            busService = new BusService(context);
        }

        //get all buses
        //api/Bus
        [Authorize(Roles ="admin,User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bus>>> GetAllBuses()
        {
            var buses = await busService.GetBuses();
            if (buses == null || !buses.Any())
            {
                return Ok(new { message = "No Bus data found." });
            }
            return Ok(buses);
        }

        //get bus by id
        //api/Bus/{id}
        [Authorize(Roles = "admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Bus>> GetBusById(int id)
        {
            var bus=await busService.FindBusById(id);
            if(bus==null)
            {
                return NotFound(new { message = $"Bus with ID {id} not found." });
            }
            return Ok(bus);
        }

        //get bus by name
        //api/Bus/search/{name}
        [Authorize(Roles = "admin,User")]
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBusByName(string name)
        {
            var buses=await busService.FindBusByName(name);
            if(buses==null||!buses.Any())
            {
                return NotFound(new { message = $"No bus found with registration or manufacturer matching this {name}." }); 
            }
            return Ok(buses);
        }


        //add bus
        //api/Bus/create
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<ActionResult> AddBus([FromBody] BusDTO dto)
        {
            var bus = new Bus()
            {
                RegistrationNumber = dto.RegistrationNumber,
                FuelType = dto.FuelType,
                Capacity = dto.Capacity,
                ModelYear = dto.ModelYear,
                Manufacturer = dto.Manufacturer
            };
           var result=await busService.AddBus(bus);
            if(result>0)
            {
                return Ok(new { message = "Bus added Successfully." });
            }
            return BadRequest(new { message = "Failed to add Bus" });
        }


        //update Bus
        //api/Bus/update/{id}
        [Authorize(Roles = "admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateBus(int id, [FromBody] BusDTO bus)
        {
            var result=await busService.UpdateBus(id, bus);
            if(result>0)
            {
                return Ok(new { message = "Bus updated Successfully." });
            }
            return NotFound(new { message = $"Bus with ID {id} not found or update failed." });
        }

        //delete bus
        //api/Bus/delete/{id}
        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteBus(int id) 
        {
            var result=await busService.DeleteBus(id);
            if(result>0)
            {
                return Ok(new { message = "Bus deleted Successfully" });
            }
            if (result == -1)
            {
                return Ok(new { message = "Cannot delete bus. It is assigned to active trips." });
            }
            return NotFound(new { message = $"Bus with ID {id} not found or delete failed" });
        }


    }
}
