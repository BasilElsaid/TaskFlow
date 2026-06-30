using TaskFlow.Enums;
using TaskStatus = TaskFlow.Enums.TaskStatus;

namespace TaskFlow.Dtos.Task;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus TaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    
    public DateTime? DueTime { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public int ProjectId { get; set; }
    public string? AssignedUserId { get; set; }
}