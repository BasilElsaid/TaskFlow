using TaskFlow.Dtos.Task;

namespace TaskFlow.Interfaces;

public interface ITaskService
{
    List<TaskDto> GetByProject(int projectId, string userId);
    TaskDto? GetById(int id,  string userId);
    TaskDto Create(CreateTaskDto dto, string userId);
    TaskDto? Update(int id, UpdateTaskDto dto, string userId);
    bool Delete(int id, string userId);
}