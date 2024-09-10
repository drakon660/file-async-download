using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace AsyncEnumerable.Core;

public interface IDomainEvent{}

public record OrderSubmitted : IDomainEvent;

public static class DomainEvents
{
    private static readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public static void Register<T>(Action<T> action)
    {
        if (_handlers.TryGetValue(typeof(T), out var delegates))
        {
            delegates.Add(action);
        }
        else
        {
            _handlers.Add(typeof(T), [action]);    
        }
    }

    public static void Raise<T>(T @event)
    {
        foreach (var handler in _handlers[typeof(T)])
        {
            Action<T> action = (Action<T>)handler;
            action(@event);
        }
    }
}


public class Order
{
    public void PlaceOrder()
    {
        DomainEvents.Raise(new OrderSubmitted());
    }
}


public class OrderNotification
{
    public OrderNotification()
    {
        DomainEvents.Register<OrderSubmitted>(Notified);
    }


    public void Notified(OrderSubmitted submitted)
    {
        Console.WriteLine("Order submitted");
    }
}

public class OrderNotification2
{
    public OrderNotification2()
    {
        DomainEvents.Register<OrderSubmitted>(Notified);
    }

    public void Notified(OrderSubmitted submitted)
    {
        Console.WriteLine("Me too Order submitted");
    }
}