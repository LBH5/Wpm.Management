using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities;

public class VitalSigns:Entity
{
    public DateTime RecordedAt { get; init; }
    public decimal Temperature { get; init; }
    public int HeartRate { get; init; }
    public int RespiratoryRate { get; init; }

    public VitalSigns() { }
    public VitalSigns(DateTime recordedAt, decimal temperature, int heartRate, int respiratoryRate)
    {
        Id = Guid.NewGuid();
        RecordedAt = recordedAt;
        Temperature = temperature;
        HeartRate = heartRate;
        RespiratoryRate = respiratoryRate;
    }
}
