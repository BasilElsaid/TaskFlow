using TaskFlow.Dtos.Project;
using TaskFlow.Models;

namespace TaskFlow.Mappers;

public static class ProjectMapper

{
    public static ProjectDto ToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
    }

    public static List<ProjectDto> ToDtoList(List<Project> projects)
    {
        return projects.Select(ToDto).ToList();
    }

    public static Project ToEntity(CreateProjectDto dto, string userId)
    {
        return new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }
}