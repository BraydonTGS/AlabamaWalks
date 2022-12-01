// Entry Point for the Application //

// Creates the WebApplication builder class //
// We can use to inject the dependencies into the services collection //
using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container //
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injecting the DbContext Class into the Services Collection //
builder.Services.AddDbContext<AlabamaWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AlabamaWalks"));
});

// Whenever I aks for the Interface - Give me this Implementation //
builder.Services.AddScoped<IRegionRepository, RegionRepositories>(); 

var app = builder.Build();

// Configure the HTTP request pipeline //
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// After the Api is running it is listening for any clients that call its endpoint //