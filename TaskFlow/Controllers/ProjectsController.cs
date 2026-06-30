using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.Project;
using TaskFlow.Interfaces;
using TaskFlow.Models;

namespace TaskFlow.Controllers;

[Authorize]
[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IActionResult GetUserProjects()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var projects = _projectService.GetUserProjects(userId!);
        return Ok(projects);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = _projectService.GetById(id, userId!);

        if (project == null)
        {
            return NotFound();
        }
        
        return Ok(project);
    }

    [HttpPost]
    public IActionResult CreateProject([FromBody] CreateProjectDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = _projectService.Create(userId!, dto.Name, dto.Description);
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = _projectService.Delete(id, userId!);

        if (!result)
        {
            return NotFound();
        }
        
        return Ok();
    }
}