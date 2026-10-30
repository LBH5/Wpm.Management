using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Api.Inftrastructure.Persistence;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Inftrastructure.Repositories;

public class PetRepository(ManagementDbContext context) : IPetRepository
{
    public async Task CreateAsync(Pet pet)
    {
        await context.Pets.AddAsync(pet);
        await context.SaveChangesAsync();
    }

    public async Task<Pet?> GetByIdAsync(Guid petId)
    {
        return await context.Pets.FirstOrDefaultAsync(p => p.Id == petId);
    }

    public async Task UpdateAsync(Pet pet)
    {
        await context.SaveChangesAsync();
    }
}
