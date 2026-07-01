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
    
    public async Task<List<TaskDto>> GetByProject(
        int projectId, string userId,  TaskFilterDto filter)
    {
        var query = BuildQuery(projectId, userId, filter);
        var tasks = await query.ToListAsync();
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
    
    private IQueryable<TaskItem> BuildQuery(
        int projectId,
        string userId,
        TaskFilterDto filter)
    {
        var query = _dbContext.TaskItems
            .Where(t =>
                t.ProjectId == projectId &&
                t.Project.OwnerId == userId)
            .AsQueryable();

        if (filter.TaskStatus.HasValue)
            query = query.Where(t => t.TaskStatus == filter.TaskStatus);

        if (filter.TaskPriority.HasValue)
            query = query.Where(t => t.TaskPriority == filter.TaskPriority);

        if (filter.AssignedToMe == true)
            query = query.Where(t => t.AssignedUserId == userId);

        if (filter.DueBefore.HasValue)
            query = query.Where(t => t.DueDate <= filter.DueBefore);

        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(t =>
                t.Title.Contains(filter.Search) ||
                t.Description.Contains(filter.Search));

        return query;
    }
}