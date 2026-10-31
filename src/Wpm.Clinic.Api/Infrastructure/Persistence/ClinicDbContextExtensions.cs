using Microsoft.EntityFrameworkCore;

namespace Wpm.Clinic.Api.Infrastructure.Persistence;

public static class ClinicDbContextExtensions
{

    extension(IApplicationBuilder app)
    {
        public async Task EnsureDatabaseCreatedAsync()
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.CloseConnectionAsync();
        }
    }
        
}
