namespace Wpm.Clinic.Domain.ValueObjects;

public record VitalSigns
{
    public DateTime RecordedAt { get; init; }
    public decimal Temperature { get; init; }
    public int HeartRate { get; init; }
    public int RespiratoryRate { get; init; }

    public VitalSigns(decimal temperature, int heartRate, int respiratoryRate)
    {
        if (temperature < 30 || temperature > 45)
        {
            throw new ArgumentException("Temperature must be between 30 and 45 degrees Celsius.", nameof(temperature));
        }
        if (heartRate < 30 || heartRate > 200)
        {
            throw new ArgumentException("Heart rate must be between 30 and 200 beats per minute.", nameof(heartRate));
        }
        if (respiratoryRate < 5 || respiratoryRate > 60)
        {
            throw new ArgumentException("Respiratory rate must be between 5 and 60 breaths per minute.", nameof(respiratoryRate));
        }
        
        RecordedAt = DateTime.UtcNow;
        Temperature = temperature;
        HeartRate = heartRate;
        RespiratoryRate = respiratoryRate;
    }
}
