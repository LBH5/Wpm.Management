using System;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;

namespace Wpm.Clinic.Api.Application.Handlers;

public class SetWeightCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<SetWeightCommand>
{
    public async Task HandleAsync(SetWeightCommand command, CancellationToken cancellationToken = default)
    {
        var consultation = await consultationRepository.LoadAsync(command.ConsultationId, cancellationToken) ?? throw new KeyNotFoundException($"Consultation with ID {command.ConsultationId} not found");
        consultation.SetWeight(command.Weight);
        await consultationRepository.SaveAsync(consultation, cancellationToken);
    }
}
