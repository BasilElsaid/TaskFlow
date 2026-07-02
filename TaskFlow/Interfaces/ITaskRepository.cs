using TaskFlow.Dtos.Requests.Task;
using TaskFlow.Models;

namespace TaskFlow.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetByProjectAsync(int projectId, string userId, TaskFilterRequest filter);
    Task<TaskItem?> GetByIdAsync(int id, string userId);
    Task AddAsync(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task SaveChangesAsync();
}