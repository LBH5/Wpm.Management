using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain;

public class Consultation: AggregateRoot
{
    private readonly List<DrugAdministration> _drugAdministrations = [];
    private readonly List<VitalSigns> _vitalSignsRecords = [];
    public DateTime StartedAt { get; init; }
    public DateTime? EndedAt { get; private set; }
    public Text? Diagnosis { get; private set; }
    public Text? Treatment { get; private set; }
    public PatientId PatientId { get; init; }
    public Weight? CurrentWeight { get; private set; }
    public ConsultaionStatus Status { get; private set; }

    public IReadOnlyList<DrugAdministration> AdministeredDrugs => _drugAdministrations.AsReadOnly();
    public IReadOnlyList<VitalSigns> VitalSignsRecords => _vitalSignsRecords.AsReadOnly();
    public Consultation(PatientId patientId )
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        Status = ConsultaionStatus.Open;
        StartedAt = DateTime.UtcNow;
    }
    public void SetDiagnosis(Text diagnosis)
    {
        ValidateConsultationStatus();
        Diagnosis = diagnosis;
    }

    public void SetTreatment(Text treatment)
    {
        ValidateConsultationStatus();
        Treatment = treatment;
    }

    public void SetWeight(Weight weight)
    {
        ValidateConsultationStatus();
        CurrentWeight = weight;
    }

    public void End()
    {
        ValidateConsultationStatus();
        if(Diagnosis == null || Treatment == null || CurrentWeight == null)
        {
            throw new InvalidOperationException("Cannot close consultation without diagnosis, treatment, and weight.");
        }
        EndedAt = DateTime.UtcNow;
        Status = ConsultaionStatus.Closed;
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

}

public enum ConsultaionStatus
{
    Open,
    Closed
}