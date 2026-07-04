using TaskFlow.Enums;

namespace TaskFlow.Dtos.Requests.Task;

public class CreateTaskRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority TaskPriority { get; set; }
    public DateTime? DueDate { get; set; }
    public int ProjectId { get; set; }
    public string? AssignedUserId { get; set; }
}