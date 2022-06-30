using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Service.ProjectService
{
    public interface IProjectService
    {
        Task AddProject(ProjectDTO projectEntity);
        Task<List<ProjectDTO>> GetProjectList();
        Task<Project> GetById(int Id);
        Task UpdateProject(ProjectDTO projectEntity);
        Task RemoveProject(int projectId);
    }
}
