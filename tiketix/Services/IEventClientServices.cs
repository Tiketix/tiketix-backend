using tiketix.Models;
using tiketix.Models.Entities;

public interface IEventClientServices
{
    Task<EventClient> GetEventClientByEmailAsync(string email);
    Task<List<EventClient>> GetAllEventClientsAsync();
    Task<EventClient> AddEventClientAsync(AddEventClientDto addEventClientDto);
    Task<EventClient> UpdateClientEmailAsync(string email, UpdateClientEmailDto updateClientEmailDto);
    Task<EventClient> UpdateClientNameAsync(string email, UpdateClientNameDto updateClientNameDto);
    Task<EventClient> UpdateClientPhoneAsync(string email, UpdateClientPhoneDto updateClientPhoneDto);
    Task<EventClient> ChangePasswordAsync(string email, ChangePasswordDto changePasswordDto);

    Task<bool> DeleteEventClientAsync(string email);

    Task<EventClient> AuthenticateAsync(string email, string password);
}