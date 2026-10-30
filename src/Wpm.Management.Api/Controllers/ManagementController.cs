using Microsoft.AspNetCore.Mvc;
using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Api.Application.Commands;

namespace Wpm.Management.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagementController(
    ICommandHandler<SetWeightCommand> setWeightCommandHandler,
    ICommandHandler<CreatePetCommand> createPetCommandHandler) : ControllerBase
{
    [HttpPost("pets/create")]
    public async Task<IActionResult> Create([FromBody] CreatePetCommand command)
    {
        // Logic to create a pet using the command
        await createPetCommandHandler.HandleAsync(command);
        return Created();
    } 

    [HttpPatch("pets/set-weight")]
    public async Task<IActionResult> SetWeight([FromBody] SetWeightCommand command)
    {
        // Logic to set the weight of a pet using the command
        await setWeightCommandHandler.HandleAsync(command);
        return NoContent();
    }
    
}
