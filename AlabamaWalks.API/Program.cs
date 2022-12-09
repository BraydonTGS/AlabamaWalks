// Entry Point for the Application //

// Creates the WebApplication builder class //
// We can use to inject the dependencies into the services collection //
using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container //
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Allowing Swagger to Set up  the Auth and Use Token everytime it makes a request // 
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] {} }
    });
});

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
builder.Services.AddScoped<ITokenHandler, AlabamaWalks.API.Repositories.TokenHandler>();

builder.Services.AddSingleton<IUserRepository, StaticUserRepository>(); 

// AutoMapper will look for all of the profiles that we have //
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Inject Authentication into the Services //
// We are defining the options of our Tolken Validations Parameters //
// Added Authentication - Added JwtBearer //
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
        ValidIssuer = builder.Configuration["Jwt:Issuer"], 
        ValidAudience = builder.Configuration["Jwt:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }); 

var app = builder.Build();

// Configure the HTTP request pipeline //
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Make sure the app is authenticated before we check for authorization //
app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();

// After the Api is running it is listening for any clients that call its endpoint //