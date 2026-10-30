using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Application.Services;

public class BreedService(IBreedRepository breedRepository) : IBreedService
{
    public async Task<Breed?> GetBreedByIdAsync(Guid breedId)
    {
        return await breedRepository.GetByIdAsync(breedId);
    }
}
