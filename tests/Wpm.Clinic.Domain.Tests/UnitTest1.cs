using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Consultation_Should_Be_Open_When_Created()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Equal(ConsultaionStatus.Open, consultation.Status);   
    }

    [Fact]
    public void Consultation_Should_Not_Have_EndedAt_When_Created()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Null(consultation.Date.EndedAt);
    }

    [Fact]
    public void Consultation_Should_Not_End_When_Missing_Data()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Throws<InvalidOperationException>(() => consultation.End());
    }

    [Fact]
    public void Consultation_Should_End_When_All_Data_Provided()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        consultation.End();
        Assert.Equal(ConsultaionStatus.Closed, consultation.Status);
        Assert.NotNull(consultation.Date.EndedAt);
    }

    [Fact]
    public void Consultation_Should_Not_Allow_Weight_Setting_When_Closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        consultation.End();
        Assert.Throws<InvalidOperationException>(() => consultation.SetWeight(17.0m));
    }

    [Fact]
    public void Consultation_Should_Not_Allow_Diagnosis_Setting_When_Closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        consultation.End();
        Assert.Throws<InvalidOperationException>(() => consultation.SetDiagnosis("New Diagnosis"));
    }

    [Fact]
    public void Consultation_Should_Not_Allow_Treatment_Setting_When_Closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        consultation.End();
        Assert.Throws<InvalidOperationException>(() => consultation.SetTreatment("New Treatment"));
    }

    [Fact]
    public void Consultation_Should_Administer_Drug_When_Open()
    {
        var consultation = new Consultation(Guid.NewGuid());
        var drugId = new DrugId(Guid.NewGuid());
        var dose = new Dose(1,UnitOfMeasure.Tablet);
        consultation.AdministredDrug(drugId, dose);
        Assert.Single(consultation.AdministeredDrugs);
        Assert.True(consultation.AdministeredDrugs[0].DrugId == drugId);
    }

    [Fact]
    public void Consultation_Should_Not_Administer_Drug_When_Closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        consultation.End();
        var drugId = new DrugId(Guid.NewGuid());
        var dose = new Dose(1,UnitOfMeasure.Tablet);
        Assert.Throws<InvalidOperationException>(() => consultation.AdministredDrug(drugId, dose));
    }

    [Fact]
    public void Consultation_Should_Register_VitalSigns_When_Open()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        IEnumerable<VitalSigns> vitalSigns = [new VitalSigns(DateTime.UtcNow,38.5m, 80, 55)];
        consultation.RegisterVitalSigns(vitalSigns);
        Assert.Single(consultation.VitalSignsRecords);
        Assert.True(consultation.VitalSignsRecords[0] == vitalSigns.First());
    }

    [Fact]
    public void Consultation_Should_Not_Register_VitalSigns_When_Closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetTreatment("Treatment");
        consultation.SetWeight(16.5m);
        consultation.End();
        IEnumerable<VitalSigns> vitalSigns = [new VitalSigns(DateTime.UtcNow,38.5m, 80, 55)];
        Assert.Throws<InvalidOperationException>(() => consultation.RegisterVitalSigns(vitalSigns));
    }

    [Fact]
    public void Consultation_DateTime_Range_Should_Not_Allow_EndDate_Before_StartDate()
    {
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddDays(-1);
        Assert.Throws<InvalidOperationException>(() => new DateTimeRange(startDate, endDate));
    }

    [Fact]
    public void Consultation_DateTime_Range_Should_Be_Ongoing_When_EndDate_Is_Null()
    {
        var startDate = DateTime.UtcNow;
        var dateTimeRange = new DateTimeRange(startDate);
        Assert.Equal("Ongoing", dateTimeRange.Duration);
    }

    [Fact]
    public void Consultation_DateTime_Range_Should_Calculate_Duration_Correctly_When_EndDate_Is_Set()
    {
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddDays(1).AddHours(2).AddMinutes(30).AddSeconds(15);
        var dateTimeRange = new DateTimeRange(startDate, endDate);
        Assert.Equal("Duration: 1 days, 2 hours, 30 minutes, 15 seconds.", dateTimeRange.Duration);
    }

    [Fact]
    public void Consultation_DateTime_Range_Should_Be_Equals()
    {
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddDays(1);
        var dateTimeRange1 = new DateTimeRange(startDate, endDate);
        var dateTimeRange2 = new DateTimeRange(startDate, endDate);
        Assert.Equal(dateTimeRange1, dateTimeRange2);
    }
}
