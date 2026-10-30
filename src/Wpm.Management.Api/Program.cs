using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Api.Application.Handlers;
using Wpm.Management.Api.Application.Services;
using Wpm.Management.Api.Inftrastructure.Persistence;
using Wpm.Management.Api.Inftrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the services and repositories
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IBreedRepository, BreedRepository>();
builder.Services.AddScoped<ICommandHandler<SetWeightCommand>, SetWeightCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreatePetCommand>, CreatePetCommandHandler>();
// Register the DbContext with the dependency injection container
builder.Services.AddDbContext<ManagementDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
   
var app = builder.Build();

// Ensure the database is created
await app.EnsureDatabaseCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
