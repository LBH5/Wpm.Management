using Wpm.Clinic.Domain.Events;
using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities;

public class Consultation: AggregateRoot
{
    private readonly List<DrugAdministration> _drugAdministrations = [];
    private readonly List<VitalSigns> _vitalSignsRecords = [];
    public DateTimeRange Date { get; private set; }
    public Text? Diagnosis { get; private set; }
    public Text? Treatment { get; private set; }
    public PatientId PatientId { get; private set; }
    public Weight? CurrentWeight { get; private set; }
    public ConsultaionStatus Status { get; private set; }

    public IReadOnlyList<DrugAdministration> AdministeredDrugs => _drugAdministrations.AsReadOnly();
    public IReadOnlyList<VitalSigns> VitalSignsRecords => _vitalSignsRecords.AsReadOnly();
    public Consultation(PatientId patientId )
    {
        ApplyDomainEvent(new ConsultationStarted(Guid.NewGuid(), patientId, DateTime.UtcNow));
    }
    public Consultation(IEnumerable<IDomainEvent> domainEvents)
    {
        LoadFromHistory(domainEvents);
    }
    public void SetDiagnosis(Text diagnosis)
    {
        ApplyDomainEvent(new DiagnosisUpdated(Id, diagnosis));
    }

    public void SetTreatment(Text treatment)
    {
        ApplyDomainEvent(new TreatmentUpdate(Id, treatment));
    }

    public void SetWeight(Weight weight)
    {
        ApplyDomainEvent(new WeightUpdate(Id, weight));
    }

    public void End()
    {
        ApplyDomainEvent(new ConsultationEnded(Id, DateTime.UtcNow));
        ValidateConsultationStatus();
    }

    public void AdministredDrug(DrugId drugId, Dose dose)
    {
        ValidateConsultationStatus();
        var drugAdministration = new DrugAdministration(drugId, dose);
        _drugAdministrations.Add(drugAdministration);
    }

    public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
    {
        ValidateConsultationStatus();
        _vitalSignsRecords.AddRange(vitalSigns);
    }

    private void ValidateConsultationStatus()
    {
        if (Status == ConsultaionStatus.Closed)
        {
            throw new InvalidOperationException("Cannot modify a closed consultation.");
        }
    }

    protected override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ConsultationStarted consultationStarted:
                Id = consultationStarted.ConsultationId;
                PatientId = consultationStarted.PatientId;
                Date = consultationStarted.StartedAt;
                Status = ConsultaionStatus.Open;
                break;
            case DiagnosisUpdated diagnosisUpdated:
                ValidateConsultationStatus();
                Diagnosis = diagnosisUpdated.Diagnosis;
                break;
            case TreatmentUpdate treatmentUpdate:
                ValidateConsultationStatus();
                Treatment = treatmentUpdate.Treatment;
                break;
            case WeightUpdate weightUpdate:
                ValidateConsultationStatus();
                CurrentWeight = weightUpdate.Weight;
                break;
            case ConsultationEnded _:
                if(Diagnosis == null || Treatment == null || CurrentWeight == null)
                {
                    throw new InvalidOperationException("Cannot close consultation without diagnosis, treatment, and weight.");
                }
                Date = new DateTimeRange(Date.StartedAt, DateTime.UtcNow);
                Status = ConsultaionStatus.Closed;
                break;
            default:
                throw new InvalidOperationException($"Unhandled domain event: {domainEvent.GetType().Name}");
        }
    }
}

public enum ConsultaionStatus
{
    Open,
    Closed
}