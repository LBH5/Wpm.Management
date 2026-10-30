using System;

namespace Wpm.Management.Api.Application.Abstractions;

public interface ICommandHandler<TCommand> where TCommand : class
{
    Task HandleAsync(TCommand command);
}
