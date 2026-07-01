using Microsoft.EntityFrameworkCore;
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
    
    public async Task<List<ProjectDto>> GetUserProjects(string userId)
    {
        var projects = await _dbContext.Projects
            .Where(p => p.OwnerId == userId)
            .ToListAsync();
        
        return ProjectMapper.ToDtoList(projects);
    }

    public async Task<ProjectDto?> GetById(int projectId, string userId)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => 
                p.Id == projectId && 
                p.OwnerId == userId);

        if (project == null)
        {
            return null;
        }
        
        return ProjectMapper.ToDto(project);
    }

    public async Task<ProjectDto?> Create(string userId, CreateProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };
        
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
        
        return ProjectMapper.ToDto(project);
    }

    public async Task<ProjectDto?> Update(int projectId, UpdateProjectDto dto, string userId)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == userId);

        if (project == null)
        {
            return null;
        }
        
        project.Name = dto.Name;
        project.Description = dto.Description;
        
        await _dbContext.SaveChangesAsync();
        return ProjectMapper.ToDto(project);
    }

    public async Task<bool> Delete(int projectId, string userId)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == userId);

        if (project == null)
        {
            return false;
        }

        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}