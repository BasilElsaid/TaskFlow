using TaskFlow.Enums;
using TaskFlow.Models;
using TaskStatus = TaskFlow.Enums.TaskStatus;

namespace TaskFlow.Dtos.Task;

public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus TaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public User? AssignedUserId { get; set; }
}