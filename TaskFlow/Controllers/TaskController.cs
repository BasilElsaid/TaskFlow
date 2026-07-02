using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.Requests.Task;
using TaskFlow.Interfaces;

namespace TaskFlow.Controllers;

[Authorize]
[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("projects/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId, [FromQuery] TaskFilterRequest filter)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var tasks = await _taskService.GetByProject(projectId, userId, filter);

        if (tasks is null)
        {
            return NotFound("Project or Tasks not found");
        }
        return Ok(tasks);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = await _taskService.GetById(id, userId);

        if (task is null)
        {
            return NotFound("Task not found");
        }
        
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = await _taskService.Create(request, userId!);

        if (task is null)
        {
            return BadRequest("Project not found");
        }
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = await _taskService.Update(id, request, userId);

        if (task is null)
        {
            return NotFound("Task not found");
        }
        
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskService.Delete(id, userId);

        if (!result)
        {
            return NotFound("Task not found");
        }

        return NoContent();
    }
}