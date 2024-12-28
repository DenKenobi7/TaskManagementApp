using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Application.Actions;

public class UpdateTestStatusAction(int id, TaskStatus newStatus)
{
    public int Id { get; } = id;
    public TaskStatus NewStatus { get; } = newStatus;
}