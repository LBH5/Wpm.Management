using Microsoft.EntityFrameworkCore;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Inftrastructure.Persistence;

public class ManagementDbContext(DbContextOptions<ManagementDbContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets => Set<Pet>();
     
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManagementDbContext).Assembly);
    }   
}
