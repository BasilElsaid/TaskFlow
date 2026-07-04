using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Dtos.Requests.Project;
using TaskFlow.Dtos.Responses.Project;
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
    
    public async Task<List<ProjectResponse>> GetUserProjects(string userId)
    {
        var projects = await _projectRepo.GetByUserAsync(userId);
        return ProjectMapper.ToResponseList(projects);
    }

    public async Task<ProjectResponse?> GetById(int projectId, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId, userId);

        if (project == null)
        {
            return null;
        }
        
        return ProjectMapper.ToResponse(project);
    }

    public async Task<ProjectResponse?> Create(string userId, CreateProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };
        
        await _projectRepo.AddAsync(project);
        await _projectRepo.SaveChangesAsync();
        
        return ProjectMapper.ToResponse(project);
    }

    public async Task<ProjectResponse?> Update(int projectId, UpdateProjectRequest request, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(projectId, userId);
        if (project == null)
        {
            return null;
        }
        
        project.Name = request.Name;
        project.Description = request.Description;
        
        await _projectRepo.SaveChangesAsync();
        return ProjectMapper.ToResponse(project);
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