using TaskFlow.Dtos.Task;

namespace TaskFlow.Interfaces;

public interface ITaskService
{
    Task<List<TaskDto>> GetByProject(int projectId, string userId, TaskFilterDto filter);
    Task<TaskDto?> GetById(int id,  string userId);
    Task<TaskDto?> Create(CreateTaskDto dto, string userId);
    Task<TaskDto?> Update(int id, UpdateTaskDto dto, string userId);
    Task<bool> Delete(int id, string userId);
}