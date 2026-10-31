namespace Wpm.Clinic.Api.Application.Commands;


public record VitalSignsRecord(DateTime RecordedAt, decimal Temperature, int HeartRate, int RespiratoryRate);
public record RegisterVitalSignsCommand(Guid ConsultationId, IEnumerable<VitalSignsRecord> VitalSignsRecords);
