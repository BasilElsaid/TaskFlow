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
    
    public List<TaskDto> GetByProject(int projectId, string userId)
    {
        var projectExists = _dbContext.Projects
            .FirstOrDefault(p => 
                p.Id == projectId && 
                p.OwnerId == userId);

        if (projectExists is null)
        {
            return new List<TaskDto>();
        }

        var tasks = _dbContext.TaskItems
            .Where(t => t.ProjectId == projectId)
            .ToList();
        
        return TaskMapper.ToDtoList(tasks);
    }

    public TaskDto? GetById(int id, string userId)
    {
        var task = _dbContext.TaskItems
            .FirstOrDefault(t => 
                t.Id == id && 
                t.Project.OwnerId == userId);

        if (task == null)
        {
            return null;
        }
        
        return TaskMapper.ToDto(task);
    }

    public TaskDto Create(CreateTaskDto dto, string userId)
    {
        var project = _dbContext.Projects
            .FirstOrDefault(p => 
                p.Id == dto.ProjectId && 
                p.OwnerId == userId);

        if (project == null)
        {
            throw new Exception("Project not found or not allowed");;
        }
        
        var task = TaskMapper.ToEntity(dto, userId);
        
        _dbContext.TaskItems.Add(task);
        _dbContext.SaveChanges();
        
        return TaskMapper.ToDto(task);
    }

    public TaskDto? Update(int id, UpdateTaskDto dto, string userId)
    {
        var task = _dbContext.TaskItems
            .FirstOrDefault(t => 
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
        task.AssignedUser = dto.AssignedUserId;
        
        _dbContext.SaveChanges();
        return TaskMapper.ToDto(task);
    }

    public bool Delete(int id, string userId)
    {
        var task = _dbContext.TaskItems
            .FirstOrDefault(t => 
                t.Id == id && 
                t.Project.OwnerId == userId);

        if (task == null)
        {
            return false;
        }
        
        _dbContext.Remove(task);
        _dbContext.SaveChanges();
        
        return true;
    }
}