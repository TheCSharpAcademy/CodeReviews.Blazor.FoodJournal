namespace FoodJournal.Services;

public class ErrorHandler
{
    public event Action<string> MessageChanged;
    private string? _messageToUI;

    public string? MessageToUI
    {
        get { return _messageToUI; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                _messageToUI = value;
            else
                _messageToUI = null;
            MessageChanged?.Invoke("changed");

        }
    }
}
