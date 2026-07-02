using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Dtos.Requests.Task;
using TaskFlow.Dtos.Responses.Project;
using TaskFlow.Dtos.Responses.Task;
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
    
    public async Task<List<TaskResponse>> GetByProject(
        int projectId, string userId,  TaskFilterRequest filter)
    {
        var tasks = await _taskRepo.GetByProjectAsync(projectId, userId, filter);
        return TaskMapper.ToResponseList(tasks);
    }

    public async Task<TaskResponse?> GetById(int id, string userId)
    {
        var task = await _taskRepo.GetByIdAsync(id, userId);

        if (task == null)
        {
            return null;
        }
        
        return TaskMapper.ToResponse(task);
    }

    public async Task<TaskResponse?> Create(CreateTaskRequest request, string userId)
    {
        var project = await _projectRepo.GetByIdAsync(request.ProjectId, userId);

        if (project == null)
        {
            return null;
        }
        
        var task = TaskMapper.ToEntity(request);
        
        await _taskRepo.AddAsync(task);
        await _taskRepo.SaveChangesAsync();
        
        return TaskMapper.ToResponse(task);
    }

    public async Task<TaskResponse?> Update(int id, UpdateTaskRequest request, string userId)
    {
        var task = await _taskRepo.GetByIdAsync(id, userId);

        if (task == null)
        {
            return null;
        }
        
        task.Title = request.Title;
        task.Description = request.Description;
        task.TaskStatus = request.TaskStatus;
        task.TaskPriority = request.TaskPriority;
        task.DueDate = request.DueDate;
        task.AssignedUserId = request.AssignedUserId;
        
        await _taskRepo.SaveChangesAsync();
        return TaskMapper.ToResponse(task);
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