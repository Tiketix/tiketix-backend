using Microsoft.EntityFrameworkCore;
using tiketix.Data;
using tiketix.Models;
using tiketix.Models.Entities;

public class EventClientServices : IEventClientServices
{
    private readonly AppDbContext _dbContext;
    
    public EventClientServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<EventClient> GetEventClientByEmailAsync(string email)
    {
        #pragma warning disable CS8603 // Possible null reference return.
        return await _dbContext.EventClients
            .FirstOrDefaultAsync(c => c.Email == email);
    }
     public async Task<List<EventClient>> GetAllEventClientsAsync()
    {
        return await _dbContext.EventClients.ToListAsync();
    }
    
    public async Task<EventClient> AddEventClientAsync(AddEventClientDto addEventClientDto)
    {
        var eventClientEntity = new EventClient()
        {
            FirstName = addEventClientDto.FirstName,
            LastName = addEventClientDto.LastName,
            Phone = addEventClientDto.Phone,
            Email = addEventClientDto.Email,
            Password = addEventClientDto.Password
        };

        await _dbContext.EventClients.AddAsync(eventClientEntity);
        await _dbContext.SaveChangesAsync();

        return eventClientEntity;
    }


    public async Task<EventClient> UpdateClientEmailAsync(string email, UpdateClientEmailDto updateClientEmailDto)
    {
        var eventClient = await _dbContext.EventClients.FirstOrDefaultAsync(c => c.Email == email);

        if (eventClient == null)
        {
        #pragma warning disable CS8603 // Possible null reference return.
            return null;
        }

        eventClient.Email = updateClientEmailDto.Email;

        await _dbContext.SaveChangesAsync();

        return eventClient;
    }

    public async Task<EventClient> UpdateClientNameAsync(string email, UpdateClientNameDto updateClientNameDto)
    {
        var eventClient = await _dbContext.EventClients.FirstOrDefaultAsync(c => c.Email == email);

        if (eventClient == null)
        {
        #pragma warning disable CS8603 // Possible null reference return.
            return null;
        }

        eventClient.FirstName = updateClientNameDto.FirstName;
        eventClient.LastName = updateClientNameDto.LastName;

        await _dbContext.SaveChangesAsync();

        return eventClient;
    }

    public async Task<EventClient> UpdateClientPhoneAsync(string email, UpdateClientPhoneDto updateClientPhoneDto)
    {
        var eventClient = await _dbContext.EventClients.FirstOrDefaultAsync(c => c.Email == email);

        if (eventClient == null)
        {
        #pragma warning disable CS8603 // Possible null reference return.
            return null;
        }

        eventClient.Phone = updateClientPhoneDto.Phone;

        await _dbContext.SaveChangesAsync();

        return eventClient;
    }

    public async Task<EventClient> ChangePasswordAsync(string email, ChangePasswordDto changePasswordDto)
    {
        var eventClient = await _dbContext.EventClients.FirstOrDefaultAsync(c => c.Email == email);

        if (eventClient == null)
        {
        #pragma warning disable CS8603 // Possible null reference return.
            return null;
        }

        eventClient.Password = changePasswordDto.Password;

        await _dbContext.SaveChangesAsync();

        return eventClient;
    }

    public async Task<bool> DeleteEventClientAsync(string email)
    {
        var eventClient = await _dbContext.EventClients.FirstOrDefaultAsync(c => c.Email == email);


        if (eventClient == null)
        {
            return false;
        }

        _dbContext.EventClients.Remove(eventClient);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<EventClient> AuthenticateAsync(string email, string password)
    {
        // Find user by email
        var eventClient = await _dbContext.EventClients
            .FirstOrDefaultAsync(c => c.Email == email);
            
        // User not found or password doesn't match
        if (eventClient == null || eventClient.Password != password)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return null;
        }
        
        // Authentication successful
        return eventClient;
    }

    
    // Implement other methods as needed


}