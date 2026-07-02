using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.Requests.Project;
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
    public async Task<IActionResult> GetUserProjects()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var projects = await _projectService.GetUserProjects(userId!);
        return Ok(projects);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = await _projectService.GetById(id, userId!);

        if (project == null)
        {
            return NotFound("Project not found");
        }
        
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = await _projectService.Create(userId!, request);
        
        return CreatedAtAction(nameof(GetById), new { id = project!.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProjectRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var project = await _projectService.Update(id, request, userId!);

        if (project == null)
        {
            return NotFound("Project not found");
        }
        
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _projectService.Delete(id, userId!);

        if (!result)
        {
            return NotFound("Project not found");
        }
        
        return NoContent();
    }
}