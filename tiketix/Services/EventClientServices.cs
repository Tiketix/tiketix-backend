using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using tiketix.Data;
using tiketix.Models;
using tiketix.Models.Entities;

public class EventClientServices : IEventClientServices
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public EventClientServices(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    // Method to generate JWT tokens
    public string GenerateJwtToken(EventClient eventClient)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
            
        #pragma warning disable CS8604 // Possible null reference argument.
        var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Key"]);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                //new Claim(ClaimTypes.NameIdentifier, eventClient.Id.ToString()),
                new Claim(ClaimTypes.Email, eventClient.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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

        object IEventClientServices.GenerateJwtToken(EventClient eventClient)
        {
            return GenerateJwtToken(eventClient);
        }


    // Implement other methods as needed


}