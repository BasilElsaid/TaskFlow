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