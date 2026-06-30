using TaskFlow.Data;
using TaskFlow.Dtos.Project;
using TaskFlow.Interfaces;
using TaskFlow.Mappers;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _dbContext;
    
    public ProjectService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<ProjectDto> GetUserProjects(string userId)
    {
        var projects = _dbContext.Projects
            .Where(p => p.OwnerId == userId)
            .ToList();
        
        return ProjectMapper.ToDtoList(projects);
    }

    public ProjectDto? GetById(int projectId, string userId)
    {
        var project = _dbContext.Projects
            .FirstOrDefault(p => p.Id == projectId && p.OwnerId == userId);

        if (project == null)
        {
            return null;
        }
        
        return ProjectMapper.ToDto(project);
    }

    public ProjectDto Create(string userId, string name, string description)
    {
        var project = new Project
        {
            Name = name,
            Description = description,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };
        
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        
        return ProjectMapper.ToDto(project);
    }

    public ProjectDto Update(int projectId, string name, string description)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int projectId, string userId)
    {
        var project = _dbContext.Projects
            .FirstOrDefault(p => p.Id == projectId && p.OwnerId == userId);

        if (project == null)
        {
            return false;
        }

        _dbContext.Remove(project);
        _dbContext.SaveChanges();
        
        return true;
    }
}