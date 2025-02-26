using tiketix.Models;
using tiketix.Models.Entities;

public interface IEventClientServices
{
    Task<EventClient> GetEventClientByEmailAsync(string email);
    Task<List<EventClient>> GetAllEventClientsAsync();
    Task<EventClient> AddEventClientAsync(AddEventClientDto addEventClientDto);
    Task<EventClient> UpdateEventClientAsync(string email, UpdateEventClientDto updateEventClientDto);
    Task<bool> DeleteEventClientAsync(string email);

    Task<EventClient> AuthenticateAsync(string email, string password);
}