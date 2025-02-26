//using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.EntityFrameworkCore;
using tiketix.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEventClientServices, EventClientServices>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddHttpsRedirection(options =>
// {
//     options.RedirectStatusCode = Status307TemporaryRedirect;
//     options.HttpsPort = 5001;
// });

builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();




app.Run();

