using TaskFlow.Dtos.Project;
using TaskFlow.Models;

namespace TaskFlow.Interfaces;

public interface IProjectService
{
    List<ProjectDto> GetUserProjects(string userId);
    ProjectDto? GetById(int projectId, string userId);
    ProjectDto Create(string userId, string name, string description);
    ProjectDto Update(int projectId, string name, string description);
    bool Delete(int projectId, string userId);
}