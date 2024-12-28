using TaskManagementApp.Domain;

namespace TaskManagementApp.Application.Interfaces;

public interface ITaskRepository
{
    Task<int> AddAsync(TaskEntity task);
    Task UpdateAsync(TaskEntity task);
    Task<TaskEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TaskEntity>> GetAllAsync();
}