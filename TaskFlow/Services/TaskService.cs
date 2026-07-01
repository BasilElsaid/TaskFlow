using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Dtos.Task;
using TaskFlow.Interfaces;
using TaskFlow.Mappers;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _dbContext;
    
    public TaskService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<TaskDto>> GetByProject(int projectId, string userId)
    {
        var projectExists = await _dbContext.Projects
            .FirstOrDefaultAsync(p => 
                p.Id == projectId && 
                p.OwnerId == userId);

        if (projectExists is null)
        {
            return null;
        }

        var tasks = await _dbContext.TaskItems
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
        
        return TaskMapper.ToDtoList(tasks);
    }

    public async Task<TaskDto?> GetById(int id, string userId)
    {
        var task = await _dbContext.TaskItems
            .FirstOrDefaultAsync(t => 
                t.Id == id && 
                t.Project.OwnerId == userId);

        if (task == null)
        {
            return null;
        }
        
        return TaskMapper.ToDto(task);
    }

    public async Task<TaskDto?> Create(CreateTaskDto dto, string userId)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => 
                p.Id == dto.ProjectId && 
                p.OwnerId == userId);

        if (project == null)
        {
            return null;
        }
        
        var task = TaskMapper.ToEntity(dto, userId);
        
        await _dbContext.TaskItems.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        
        return TaskMapper.ToDto(task);
    }

    public async Task<TaskDto?> Update(int id, UpdateTaskDto dto, string userId)
    {
        var task = await _dbContext.TaskItems
            .FirstOrDefaultAsync(t => 
                t.Id == id && 
                t.Project.OwnerId == userId);

        if (task == null)
        {
            return null;
        }
        
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.TaskStatus = dto.TaskStatus;
        task.TaskPriority = dto.TaskPriority;
        task.DueDate = dto.DueDate;
        task.AssignedUserId = dto.AssignedUserId;
        
        await _dbContext.SaveChangesAsync();
        return TaskMapper.ToDto(task);
    }

    public async Task<bool> Delete(int id, string userId)
    {
        var task = await _dbContext.TaskItems
            .FirstOrDefaultAsync(t => 
                t.Id == id && 
                t.Project.OwnerId == userId);

        if (task == null)
        {
            return false;
        }
        
        _dbContext.TaskItems.Remove(task);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}