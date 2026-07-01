using System.ComponentModel.DataAnnotations;
using TaskFlow.Enums;

namespace TaskFlow.Dtos.Task;

public class CreateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    public TaskPriority TaskPriority { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public int ProjectId { get; set; }
    public string? AssignedUserId { get; set; }
}