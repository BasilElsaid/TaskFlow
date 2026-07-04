using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Interfaces;
using TaskFlow.Models;

namespace TaskFlow.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;
    
    public ProjectRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Project>> GetByUserAsync(string userId)
    {
        return await _dbContext.Projects
            .Where(p => p.OwnerId == userId)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int projectId, string userId)
    {
        return await _dbContext.Projects
            .FirstOrDefaultAsync(p => 
                p.Id == projectId &&
                p.OwnerId == userId);
    }

    public async Task AddAsync(Project project)
    {
        await  _dbContext.Projects.AddAsync(project);
    }

    public void Update(Project project)
    {
        _dbContext.Projects.Update(project);
    }

    public void Delete(Project project)
    {
        _dbContext.Projects.Remove(project);
    }

    public async Task SaveChangesAsync()
    {
        await  _dbContext.SaveChangesAsync();
    }
}