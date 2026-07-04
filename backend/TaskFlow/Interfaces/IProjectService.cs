using TaskFlow.Dtos.Requests.Project;
using TaskFlow.Dtos.Responses.Project;
using TaskFlow.Models;

namespace TaskFlow.Interfaces;

public interface IProjectService
{
    Task<List<ProjectResponse>> GetUserProjects(string userId);
    Task<ProjectResponse?> GetById(int projectId, string userId);
    Task<ProjectResponse?> Create(string userId, CreateProjectRequest request);
    Task<ProjectResponse?> Update(int projectId, UpdateProjectRequest request, string userId);
    Task<bool> Delete(int projectId, string userId);
}