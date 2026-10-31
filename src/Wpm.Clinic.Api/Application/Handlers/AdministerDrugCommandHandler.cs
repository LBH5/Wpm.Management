using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;
using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Api.Application.Handlers;

public class AdministerDrugCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<AdministerDrugCommand>
{
    public async Task HandleAsync(AdministerDrugCommand command, CancellationToken cancellationToken = default)
    {
        var consultation = await consultationRepository.LoadAsync(command.ConsultationId, cancellationToken) ?? throw new KeyNotFoundException($"Consultation with ID {command.ConsultationId} not found");
        consultation.AdministredDrug(command.DrugId, new Dose(command.Quantity, command.UnitOfMeasure));
        await consultationRepository.SaveAsync(consultation, cancellationToken);
    }
}
