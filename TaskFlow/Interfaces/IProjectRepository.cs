using TaskFlow.Models;

namespace TaskFlow.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetByUserAsync(string userId);
    Task<Project?> GetByIdAsync(int projectId, string userId);
    Task AddAsync(Project project);
    void Update(Project project);
    void Delete(Project project);
    Task SaveChangesAsync();
}