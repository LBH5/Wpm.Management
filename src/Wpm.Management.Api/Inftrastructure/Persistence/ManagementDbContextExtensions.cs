using Microsoft.EntityFrameworkCore;

namespace Wpm.Management.Api.Inftrastructure.Persistence;

public static class ManagementDbContextExtensions
{
    extension(IApplicationBuilder app)
    {
        public async Task EnsureDatabaseCreatedAsync()
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ManagementDbContext>();
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.CloseConnectionAsync();
        }
    }
        
}
