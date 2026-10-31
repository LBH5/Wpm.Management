using Microsoft.AspNetCore.Mvc;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;

namespace Wpm.Clinic.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicController(
    ICommandHandler<StartConsultationCommand, Guid> startConsultationCommandHandler,
    ICommandHandler<EndConsultationCommand> endConsultationCommandHandler,
    ICommandHandler<SetDiagnosisCommand> setDiagnosisCommandHandler,
    ICommandHandler<SetTreatmentCommand> setTreatmentCommandHandler,
    ICommandHandler<SetWeightCommand> setWeightCommandHandler,
    ICommandHandler<AdministerDrugCommand> administerDrugCommandHandler,
    ICommandHandler<RegisterVitalSignsCommand> registerVitalSignsCommandHandler,
    ILogger<ClinicController> logger) : ControllerBase
{
    [HttpPost("consultations/start")]
    public async Task<IActionResult> StartConsultation([FromBody] StartConsultationCommand command)
    {
        try
        {
            var consultationId = await startConsultationCommandHandler.HandleAsync(command);
            return Ok(consultationId);
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error starting consultation for patient {PatientId}", command.PatientId);
            return BadRequest();
        }
    }

    [HttpPatch("consultations/end")]
    public async Task<IActionResult> EndConsultation([FromBody] EndConsultationCommand command)
    {
        try
        {
            await endConsultationCommandHandler.HandleAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error ending consultation for consultation {ConsultationId}", command.ConsultationId);
            return BadRequest();
        }
    }

    [HttpPatch("consultations/diagnosis")]
    public async Task<IActionResult> SetDiagnosis([FromBody] SetDiagnosisCommand command)
    {
        try
        {
            await setDiagnosisCommandHandler.HandleAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error setting diagnosis for consultation {ConsultationId}", command.ConsultationId);
            return BadRequest();
        }
    }

    [HttpPatch("consultations/treatment")]
    public async Task<IActionResult> SetTreatment([FromBody] SetTreatmentCommand command)
    {
        try
        {
            await setTreatmentCommandHandler.HandleAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error setting treatment for consultation {ConsultationId}", command.ConsultationId);
            return BadRequest();
        }
    }
    [HttpPatch("consultations/weight")]
    public async Task<IActionResult> SetWeight([FromBody] SetWeightCommand command)
    {
        try
        {
            await setWeightCommandHandler.HandleAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error setting weight for consultation {ConsultationId}", command.ConsultationId);
            return BadRequest();
        }
    }

    [HttpPatch("consultations/administerDrug")]
    public async Task<IActionResult> AdministerDrug([FromBody] AdministerDrugCommand command)
    {
        try
        {
            await administerDrugCommandHandler.HandleAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error administering drug for consultation {ConsultationId}", command.ConsultationId);
            return BadRequest();
        }
    }


    [HttpPatch("consultations/registerVitalSigns")]
    public async Task<IActionResult> RegisterVitalSigns([FromBody] RegisterVitalSignsCommand command)
    {
        try
        {
            await registerVitalSignsCommandHandler.HandleAsync(command);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception 
            logger.LogError(ex, "Error registering vital signs for consultation {ConsultationId}", command.ConsultationId);
            return BadRequest();
        }
    }

}
