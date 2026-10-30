using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Application.Abstractions;

public interface IPetRepository
{
    Task<Pet?> GetByIdAsync(Guid petId);
    Task CreateAsync(Pet pet);
    Task UpdateAsync(Pet pet);
}
