using TaskFlow.Dtos.Project;
using TaskFlow.Models;

namespace TaskFlow.Interfaces;

public interface IProjectService
{
    Task<List<ProjectDto>> GetUserProjects(string userId);
    Task<ProjectDto?> GetById(int projectId, string userId);
    Task<ProjectDto?> Create(string userId, CreateProjectDto dto);
    Task<ProjectDto?> Update(int projectId, UpdateProjectDto dto, string userId);
    Task<bool> Delete(int projectId, string userId);
}