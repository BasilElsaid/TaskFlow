using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.Task;
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
    public IActionResult GetByProject(int projectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var tasks = _taskService.GetByProject(projectId, userId);
        return Ok(tasks);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = _taskService.GetById(id, userId);

        if (task is null)
        {
            return NotFound();
        }
        
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = _taskService.Create(dto, userId);
        return Ok(task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateTaskDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = _taskService.Update(id, dto, userId);

        if (task is null)
        {
            return NotFound();
        }
        
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = _taskService.Delete(id, userId);

        if (!result)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}