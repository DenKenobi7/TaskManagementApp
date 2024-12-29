namespace TaskManagementApp.Domain;

public class TaskEntity(string name, string description, string? assignedTo = null)
{
    public int ID { get; set; }
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    public string? AssignedTo { get; set; } = assignedTo;

    public bool TryUpdateStatus(TaskStatus newStatus)
    {
        if (Status == newStatus)
        {
            return false;
        }

        Status = newStatus;
        return true;
    }
}