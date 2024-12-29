using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Application.Events;

public class TaskStatusUpdatedEvent(int id, TaskStatus oldStatus, TaskStatus newStatus, DateTime timestamp)
{
    public int Id { get; } = id;
    public TaskStatus OldStatus { get; } = oldStatus;
    public TaskStatus NewStatus { get; } = newStatus;
    public DateTime Timestamp { get; } = timestamp;
}