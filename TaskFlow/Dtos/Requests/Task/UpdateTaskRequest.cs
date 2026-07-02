using System.ComponentModel.DataAnnotations;
using TaskFlow.Enums;
using TaskStatus = TaskFlow.Enums.TaskStatus;

namespace TaskFlow.Dtos.Requests.Task;

public class UpdateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus TaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AssignedUserId { get; set; }
}