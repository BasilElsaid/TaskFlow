using TaskFlow.Dtos.Task;
using TaskFlow.Models;

namespace TaskFlow.Mappers;

public static class TaskMapper
{
    public static TaskDto ToDto(TaskItem task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            TaskStatus = task.TaskStatus,
            TaskPriority = task.TaskPriority,
            DueTime = task.DueDate,
            CreatedAt = task.CreatedAt,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
        };
    }

    public static List<TaskDto> ToDtoList(List<TaskItem> tasks)
    {
        return tasks.Select(ToDto).ToList();
    }

    public static TaskItem ToEntity(CreateTaskDto dto, string userId)
    {
        return new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description ?? string.Empty,
            TaskPriority = dto.TaskPriority,
            CreatedAt = DateTime.UtcNow,
            ProjectId = dto.ProjectId,
            AssignedUserId = dto.AssignedUserId,
            TaskStatus = Enums.TaskStatus.Todo,
        };
    }
}