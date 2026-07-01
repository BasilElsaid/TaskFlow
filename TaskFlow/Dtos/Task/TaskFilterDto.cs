using TaskFlow.Enums;
using TaskStatus = TaskFlow.Enums.TaskStatus;

namespace TaskFlow.Dtos.Task;

public class TaskFilterDto
{
    public TaskStatus? TaskStatus { get; set; }
    public TaskPriority? TaskPriority { get; set; }
    public bool? AssignedToMe { get; set; }
    public DateTime? DueBefore { get; set; }
    public string? Search { get; set; }
}