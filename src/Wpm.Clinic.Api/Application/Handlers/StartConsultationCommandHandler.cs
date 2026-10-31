using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;
using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Application.Handlers;


public class StartConsultationCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<StartConsultationCommand, Guid>
{
    public async Task<Guid> HandleAsync(StartConsultationCommand command, CancellationToken cancellationToken)
    {
        var consultation = new Consultation(command.PatientId);
        await consultationRepository.SaveAsync(consultation, cancellationToken);
        return consultation.Id;
    }
}
