using Microsoft.AspNetCore.Mvc;
using tiketix.Models;


namespace tiketix.Controllers
{
    // localhost:****/api/eventclients
    [Route("api/[controller]")]
    [ApiController]

    public class EventClientsController : ControllerBase
    {
        private readonly IEventClientServices _eventClientServices;

        public EventClientsController(IEventClientServices eventClientServices)
    {
        _eventClientServices = eventClientServices;
    }


        [HttpGet]
        [Route("by-email")]
        public async Task<IActionResult> GetEventClientByEmail([FromQuery] string email) 
        {
            var eventClient = await _eventClientServices.GetEventClientByEmailAsync(email);

            if (eventClient is null)
            {
                return NotFound("Client with this email does not exist");
            }

            return Ok(eventClient);
        }

        [HttpGet]
    public async Task<IActionResult> GetAllEventClients()
    {
        var eventClients = await _eventClientServices.GetAllEventClientsAsync();
        return Ok(eventClients);
    }
    

    [HttpPost]
    public async Task<IActionResult> AddEventClient(AddEventClientDto addEventClientDto)
    {
        var eventClient = await _eventClientServices.AddEventClientAsync(addEventClientDto);
        return Ok(eventClient);
    }

    [HttpPut]
    [Route("update-by-email")]
    public async Task<IActionResult> UpdateEventClient(string email, UpdateEventClientDto updateEventClientDto)
    {
        var eventClient = await _eventClientServices.UpdateEventClientAsync(email, updateEventClientDto);

        if (eventClient is null)
        {
            return NotFound("Client was not found");
        }

        return Ok(eventClient);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteEventClient(string email)
    {
        var result = await _eventClientServices.DeleteEventClientAsync(email);

        if (!result)
        {
            return NotFound("Client not found");
        }

        return Ok("Deleted Successfully");
    }
    

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var eventClient = await _eventClientServices.AuthenticateAsync(loginDto.Email, loginDto.Password);
        
        if (eventClient == null)
        {
            return Unauthorized("Invalid email or password");
        }
        
        // Authentication successful
        // Create a response without including sensitive information
        var response = new
        {
            eventClient.Id,
            eventClient.Email,
            eventClient.FirstName,
            eventClient.LastName
        };
        
        return Ok(response);
    }
}
}
