using TaskFlow.Dtos.Requests.Task;
using TaskFlow.Dtos.Responses.Project;
using TaskFlow.Dtos.Responses.Task;

namespace TaskFlow.Interfaces;

public interface ITaskService
{
    Task<List<TaskResponse>> GetByProject(int projectId, string userId, TaskFilterRequest filter);
    Task<TaskResponse?> GetById(int id,  string userId);
    Task<TaskResponse?> Create(int projectId, CreateTaskRequest request, string userId);
    Task<TaskResponse?> Update(int id, UpdateTaskRequest request, string userId);
    Task<bool> Delete(int id, string userId);
}