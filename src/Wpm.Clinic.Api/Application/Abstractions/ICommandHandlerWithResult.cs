namespace Wpm.Clinic.Api.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : class
{
    Task<TResult> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}