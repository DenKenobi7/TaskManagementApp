namespace TaskManagementApp.ServiceBus.Models;

public record class SendMessageResponse
{
    public bool IsSucceeded { get; set; }

    public string? ExceptionMessage { get; set; }
}
