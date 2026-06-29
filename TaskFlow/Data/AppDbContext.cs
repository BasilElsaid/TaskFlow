using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;

namespace TaskFlow.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Project -> Owner
        builder.Entity<Project>()
            .HasOne(p => p.Owner)
            .WithMany(u => u.OwnedProjects)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Project -> Tasks
        builder.Entity<TaskItem>()
            .HasOne(ti => ti.Project)
            .WithMany(ta => ta.Tasks)
            .HasForeignKey(f => f.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Task -> AssignedUser
        builder.Entity<TaskItem>()
            .HasOne(t => t.AssignedUser)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedUserId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // ProjectMember PK
        builder.Entity<ProjectMember>()
            .HasKey(pm => new { pm.UserId, pm.ProjectId });
        
        // User -> ProjectMember
        builder.Entity<ProjectMember>()
            .HasOne(pm => pm.User)
            .WithMany(u => u.ProjectMemberships)
            .HasForeignKey(pm => pm.UserId);
        
        // Project -> ProjectMember
        builder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project)
            .WithMany(p => p.ProjectMembers)
            .HasForeignKey(pm => pm.ProjectId);
    }
}