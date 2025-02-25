using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tiketix.Data;
using tiketix.Models;
using tiketix.Models.Entities;

namespace tiketix.Controllers
{
    // localhost:****/api/eventclients
    [Route("api/[controller]")]
    [ApiController]

    public class EventClientsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EventClientsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEventClients()
        {
            return Ok(dbContext.EventClients.ToList());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetEventClientById(Guid id) 
        {
            var eventClient = dbContext.EventClients.Find(id);

            if (eventClient is null)
            {
                return NotFound("Client does not exist");
            }

            return Ok(eventClient);
        }
     
        [HttpPost]
        public IActionResult AddEventClient(AddEventClientDto addEventClientDto)
        {
            var eventClientEntity = new EventClient()
            {
                FirstName = addEventClientDto.FirstName,
                LastName = addEventClientDto.LastName,
                Phone = addEventClientDto.Phone,
                Email = addEventClientDto.Email,
                Password = addEventClientDto.Password
            };

            dbContext.EventClients.Add(eventClientEntity);
            dbContext.SaveChanges();

            return Ok(eventClientEntity);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateEventClient(Guid id, UpdateEventClientDto updateEventClientDto)
        {
            var eventClient = dbContext.EventClients.Find(id);

            if (eventClient is null)
            {
                return NotFound("Client was not found");
            }

            eventClient.FirstName = updateEventClientDto.FirstName;
            eventClient.LastName = updateEventClientDto.LastName;
            eventClient.Email = updateEventClientDto.Email;
            eventClient.Phone = updateEventClientDto.Phone;
            eventClient.Password = updateEventClientDto.Password;

            dbContext.SaveChanges();

            return Ok(eventClient);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteEventClient(Guid id)
        {
            var eventClient = dbContext.EventClients.Find(id);

            if (eventClient is null)
            {
                return NotFound("Client not found");
            }

            dbContext.EventClients.Remove(eventClient);
            dbContext.SaveChanges();

            return Ok("Deleted Successfully");
        }
    }
}
