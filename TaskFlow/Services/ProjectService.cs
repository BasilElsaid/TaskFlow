using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Dtos.Project;
using TaskFlow.Interfaces;
using TaskFlow.Mappers;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepo;
    
    public ProjectService(IProjectRepository projectRepo)
    {
        _projectRepo = projectRepo;
    }
    
    public async Task<List<ProjectDto>> GetUserProjects(string userId)
    {
        var projects = await _projectRepo.GetByUserAsync(userId);
        return ProjectMapper.ToDtoList(projects);
    }

    public async Task<ProjectDto?> GetById(int projectId, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId, userId);

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
        
        await _projectRepo.AddAsync(project);
        await _projectRepo.SaveChangesAsync();
        
        return ProjectMapper.ToDto(project);
    }

    public async Task<ProjectDto?> Update(int projectId, UpdateProjectDto dto, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId, userId);
        if (project == null)
        {
            return null;
        }
        
        project.Name = dto.Name;
        project.Description = dto.Description;
        
        await _projectRepo.SaveChangesAsync();
        return ProjectMapper.ToDto(project);
    }

    public async Task<bool> Delete(int projectId, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId, userId);
        if (project == null)
        {
            return false;
        }

        _projectRepo.Delete(project);
        await _projectRepo.SaveChangesAsync();
        
        return true;
    }
}