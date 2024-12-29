using TaskManagementApp.Domain;

namespace TaskManagementApp.Application.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskEntity task, CancellationToken cancellationToken);
    Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken);
    Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
}