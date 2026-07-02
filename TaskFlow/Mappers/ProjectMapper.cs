using TaskFlow.Dtos.Requests.Project;
using TaskFlow.Dtos.Responses.Project;
using TaskFlow.Models;

namespace TaskFlow.Mappers;

public static class ProjectMapper

{
    public static ProjectResponse ToResponse(Project project)
    {
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
    }

    public static List<ProjectResponse> ToResponseList(List<Project> projects)
    {
        return projects.Select(ToResponse).ToList();
    }

    public static Project ToEntity(CreateProjectRequest request, string userId)
    {
        return new Project
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }
}