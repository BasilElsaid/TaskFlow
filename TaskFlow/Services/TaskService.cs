using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Dtos.Task;
using TaskFlow.Interfaces;
using TaskFlow.Mappers;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepo;
    private readonly IProjectRepository _projectRepo;
    
    public TaskService(ITaskRepository taskRepo, IProjectRepository projectRepo)
    {
        _taskRepo = taskRepo;
        _projectRepo = projectRepo;
    }
    
    public async Task<List<TaskDto>> GetByProject(
        int projectId, string userId,  TaskFilterDto filter)
    {
        var tasks = await _taskRepo.GetByProjectAsync(projectId, userId, filter);
        return TaskMapper.ToDtoList(tasks);
    }

    public async Task<TaskDto?> GetById(int id, string userId)
    {
        var task = await _taskRepo.GetByIdAsync(id, userId);

        if (task == null)
        {
            return null;
        }
        
        return TaskMapper.ToDto(task);
    }

    public async Task<TaskDto?> Create(CreateTaskDto dto, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(dto.ProjectId, userId);

        if (project == null)
        {
            return null;
        }
        
        var task = TaskMapper.ToEntity(dto, userId);
        
        await _taskRepo.AddAsync(task);
        await _taskRepo.SaveChangesAsync();
        
        return TaskMapper.ToDto(task);
    }

    public async Task<TaskDto?> Update(int id, UpdateTaskDto dto, string userId)
    {
        var task = await _taskRepo.GetByIdAsync(id, userId);

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
        
        await _taskRepo.SaveChangesAsync();
        return TaskMapper.ToDto(task);
    }

    public async Task<bool> Delete(int id, string userId)
    {
        var task = await _taskRepo.GetByIdAsync(id, userId);

        if (task == null)
        {
            return false;
        }
        
        _taskRepo.Delete(task);
        await _taskRepo.SaveChangesAsync();
        
        return true;
    }
}