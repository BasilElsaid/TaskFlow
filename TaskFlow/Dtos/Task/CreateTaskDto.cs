using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using TaskFlow.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskFlow.Dtos.Task;

public class CreateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    public TaskPriority TaskPriority { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public int ProjectId { get; set; }
    public string? AssignedUserId { get; set; }
}