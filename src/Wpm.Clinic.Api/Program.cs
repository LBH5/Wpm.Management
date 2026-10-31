using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;
using Wpm.Clinic.Api.Application.Handlers;
using Wpm.Clinic.Api.Infrastructure.Persistence;
using Wpm.Clinic.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the ClinicDbContext with the dependency injection container
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ClinicDatabase")));
// Register the ConsultationRepository with the dependency injection container
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
// Register the StartConsultationCommandHandler with the dependency injection container
builder.Services.AddScoped<ICommandHandler<StartConsultationCommand, Guid>, StartConsultationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<EndConsultationCommand>, EndConsultationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SetDiagnosisCommand>, SetDiagnosisCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SetTreatmentCommand>, SetTreatmentCommandHandler>();
builder.Services.AddScoped<ICommandHandler<SetWeightCommand>, SetWeightCommandHandler>();
builder.Services.AddScoped<ICommandHandler<AdministerDrugCommand>, AdministerDrugCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterVitalSignsCommand>, RegisterVitalSignsCommandHandler>();
//
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
