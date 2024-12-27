namespace TaskManagementApp.Application.DTOs;

public class TaskEntityDto
{
    public int ID { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TaskStatus Status { get; set; }
    public string? AssignedTo { get; set; }
}