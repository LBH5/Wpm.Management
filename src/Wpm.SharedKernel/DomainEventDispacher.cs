namespace Wpm.SharedKernel;


public class DomainEventDispacher<T> where T : IDomainEvent
{
    private List<Action<T>> Actions { get; } = [];

    public void Subscribe(Action<T> action)
    {
        if(Actions.Exists(a => a == action))
        {
           return;
        }
        Actions.Add(action);
    }

    public void Publish(T args)
    {
        foreach (var action in Actions)
        {
            action.Invoke(args);
        }
    }
}
