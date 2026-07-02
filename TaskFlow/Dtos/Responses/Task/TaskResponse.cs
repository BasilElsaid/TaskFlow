using TaskFlow.Enums;
using TaskStatus = TaskFlow.Enums.TaskStatus;

namespace TaskFlow.Dtos.Responses.Task;

public class TaskResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus TaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public int ProjectId { get; set; }
    public string? AssignedUserId { get; set; }
}