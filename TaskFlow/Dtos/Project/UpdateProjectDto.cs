using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Dtos.Project;

public class UpdateProjectDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}