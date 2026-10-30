using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Application.Abstractions;

public interface IBreedService
{
    Task<Breed?> GetBreedByIdAsync(Guid breedId);
}
