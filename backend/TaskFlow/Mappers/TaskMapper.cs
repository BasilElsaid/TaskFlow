using TaskFlow.Dtos.Requests.Task;
using TaskFlow.Dtos.Responses.Task;
using TaskFlow.Models;

namespace TaskFlow.Mappers;

public static class TaskMapper
{
    public static TaskResponse ToResponse(TaskItem task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            TaskStatus = task.TaskStatus,
            TaskPriority = task.TaskPriority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
        };
    }

    public static List<TaskResponse> ToResponseList(List<TaskItem> tasks)
    {
        return tasks.Select(ToResponse).ToList();
    }

    public static TaskItem ToEntity(CreateTaskRequest request)
    {
        return new TaskItem
        {
            Title = request.Title,
            Description = request.Description ?? string.Empty,
            TaskPriority = request.TaskPriority,
            CreatedAt = DateTime.UtcNow,
            ProjectId = request.ProjectId,
            AssignedUserId = request.AssignedUserId,
            TaskStatus = Enums.TaskStatus.Todo,
        };
    }
}