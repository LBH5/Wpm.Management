namespace Wpm.SharedKernel;

public abstract class AggregateRoot: Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public int Version { get; private set; } 
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void ApplyDomainEvent(IDomainEvent domainEvent)
    {
        ChangeStateByUsingDomainEvent(domainEvent);
        _domainEvents.Add(domainEvent);
        Version++;
    }

    public void LoadFromHistory(IEnumerable<IDomainEvent> history)
    {
        foreach (var domainEvent in history)
        {
            ApplyDomainEvent(domainEvent);
        }
        ClearDomainEvents();
    }

    protected abstract void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent);

}
