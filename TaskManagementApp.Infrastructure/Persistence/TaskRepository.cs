using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Domain;

namespace TaskManagementApp.Infrastructure.Persistence
{
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        public async Task AddAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            await context.Tasks.AddAsync(task, cancellationToken);
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Tasks.ToListAsync(cancellationToken);
        }

        public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Tasks.FindAsync(id, cancellationToken);
        }

        public Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            context.Tasks.Update(task);
            return Task.CompletedTask;
        }
    }
}
