namespace AsyncEnumerable.Core;



public class Button
{
    public event EventHandler? OnClick;

    public void Click()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
    }
}

public interface IDispatcher {}

public class Dispatcher : IDispatcher
{
    private IList<Action> _actions = new List<Action>();

    public void Subscribe(ISubscriber subscriber)
    {
        
    }
    
    public void Dispatch(ICommand command)
    {
        
    }
}

public interface ICommand
{
    
}

public record ClickCommand() : ICommand;

public class ButtonPublisher
{
    private readonly Dispatcher _dispatcher;

    public ButtonPublisher(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    public void Click()
    {
        _dispatcher.Dispatch(new ClickCommand());
    }
}

public interface ISubscriber
{
    
}

public class ButtonSubscriber : ISubscriber 
{
    public void Subscribe(IDispatcher dispatcher)
    {
        
    }
}