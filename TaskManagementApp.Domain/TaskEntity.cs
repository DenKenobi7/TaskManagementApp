namespace TaskManagementApp.Domain;

public class TaskEntity
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    public string? AssignedTo { get; set; }

    public TaskEntity() { }

    public TaskEntity(int id, string name, string description, string? assignedTo = null)
    {
        ID = id;
        Name = name;
        Description = description;
        AssignedTo = assignedTo;    
        Status = TaskStatus.NotStarted;
    }

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