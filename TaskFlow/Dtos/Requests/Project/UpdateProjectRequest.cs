using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Dtos.Requests.Project;

public class UpdateProjectRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}