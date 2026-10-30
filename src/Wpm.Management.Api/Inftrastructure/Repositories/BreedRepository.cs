using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Inftrastructure.Repositories;

public class BreedRepository : IBreedRepository
{
    public readonly List<Breed> breeds =
    [
        new Breed(Guid.Parse("df3b8c1e-4c2a-4f5b-9d6e-1a2b3c4d5e6f"),
            "Labrador Retriever",
            "Friendly and outgoing",
            new WeightRange(55, 80),
            new WeightRange(50, 70)),
        new Breed(Guid.Parse("a1b2c3d4-4c2a-4f5b-9d6e-1a2b3c4d5e6f"),
            "German Shepherd",
            "Intelligent and versatile",
            new WeightRange(65, 90),
            new WeightRange(50, 70)),
        new Breed(Guid.Parse("c8d1e2f3-4c2a-4f5b-9d6e-1a2b3c4d5e6f"),
            "Golden Retriever",
            "Friendly and tolerant",
            new WeightRange(55, 75),
            new WeightRange(50, 70))
    ];

    public async Task<Breed?> GetByIdAsync(Guid breedId)
    {
        if (breedId == Guid.Empty)
        {
            throw new ArgumentException("Breed ID cannot be empty.", nameof(breedId));
        }
        var breed = breeds.FirstOrDefault(b => b.Id == breedId);
        return breed ?? throw new KeyNotFoundException($"Breed with ID {breedId} not found.");
    }
}
