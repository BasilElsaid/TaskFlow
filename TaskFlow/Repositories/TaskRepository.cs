using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Dtos.Task;
using TaskFlow.Interfaces;
using TaskFlow.Models;

namespace TaskFlow.Repositories;

public class TaskRepository : ITaskRepository
{
    
    private readonly AppDbContext _dbContext;
    
    public TaskRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<TaskItem>> GetByProjectAsync(int projectId, string userId, TaskFilterDto filter)
    {
        var query = _dbContext.TaskItems
            .Where(t => t.ProjectId == projectId && t.Project.OwnerId == userId);

        if (filter.TaskStatus.HasValue)
            query = query.Where(t => t.TaskStatus == filter.TaskStatus);

        if (filter.TaskPriority.HasValue)
            query = query.Where(t => t.TaskPriority == filter.TaskPriority);

        if (filter.AssignedToMe == true)
            query = query.Where(t => t.AssignedUserId == userId);

        if (filter.DueBefore.HasValue)
            query = query.Where(t => t.DueDate <= filter.DueBefore);

        return await query.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id, string userId)
    {
        return await _dbContext.TaskItems
            .FirstOrDefaultAsync(t => t.Id == id && t.Project.OwnerId == userId);
    }

    public async Task AddAsync(TaskItem task)
    {
        await _dbContext.TaskItems.AddAsync(task);
    }

    public void Update(TaskItem task)
    {
        _dbContext.TaskItems.Update(task);
    }

    public void Delete(TaskItem task)
    {
        _dbContext.TaskItems.Remove(task);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}