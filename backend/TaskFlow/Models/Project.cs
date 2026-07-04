namespace TaskFlow.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string OwnerId { get; set; } =  string.Empty;
    public User Owner { get; set; } = null!;
    
    public List<TaskItem> Tasks { get; set; } = [];
    public List<ProjectMember> ProjectMembers { get; set; } = [];
}