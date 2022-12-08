// Entry Point for the Application //

// Creates the WebApplication builder class //
// We can use to inject the dependencies into the services collection //
using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container //
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Fluent Validation Notes
// Injecting Fluent Validation - Registering Validators across my Program //
// Because in our controller we are using the ApiController Tag when the execution //
// comes to a method it uses the tag to check that the model state is valid or invalid //
// If it fails it will set the model state to invalid and the execution will never come //
// into the method //
#endregion
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Injecting the DbContext Class into the Services Collection //
builder.Services.AddDbContext<AlabamaWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AlabamaWalks"));
});

// Whenever I aks for the Interface - Give me this Implementation //
// Inject into the services of the application //
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepositories>();
builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();

// AutoMapper will look for all of the profiles that we have //
builder.Services.AddAutoMapper(typeof(Program).Assembly); 

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