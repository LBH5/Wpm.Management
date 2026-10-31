using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;

namespace Wpm.Clinic.Api.Application.Handlers;


public class StartConsultationCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<StartConsultationCommand, Guid>
{
    public async Task<Guid> HandleAsync(StartConsultationCommand command, CancellationToken cancellationToken)
    {
        return await consultationRepository.CreateAsync(command.PatientId, cancellationToken);
    }
}
