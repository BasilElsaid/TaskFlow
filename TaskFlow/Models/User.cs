using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Project> OwnedProjects { get; set; } = [];
    public List<ProjectMember> ProjectMemberships { get; set; } = [];
    public List<TaskItem> AssignedTasks { get; set; } = [];
}