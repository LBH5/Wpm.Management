using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Application.Abstractions;

public interface IBreedRepository
{
    Task<Breed?> GetByIdAsync(Guid breedId);
}
