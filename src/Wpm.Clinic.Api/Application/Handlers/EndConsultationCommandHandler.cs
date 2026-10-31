using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;

namespace Wpm.Clinic.Api.Application.Handlers;

public class EndConsultationCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<EndConsultationCommand>
{
    public async Task HandleAsync(EndConsultationCommand command, CancellationToken cancellationToken = default)
    {
        var consultation = await consultationRepository.GetByIdAsync(command.ConsultationId, cancellationToken) ?? throw new KeyNotFoundException($"Consultation with ID {command.ConsultationId} not found");
        consultation.End();
        await consultationRepository.UpdateAsync(consultation, cancellationToken);
    }
}
