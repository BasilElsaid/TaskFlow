using System.ComponentModel.DataAnnotations;
using TaskFlow.Enums;
using TaskFlow.Models;
using TaskStatus = TaskFlow.Enums.TaskStatus;

namespace TaskFlow.Dtos.Task;

public class UpdateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus TaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public string? AssignedUserId { get; set; }
}