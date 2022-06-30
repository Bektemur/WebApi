using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Service.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProjectService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddProject(ProjectDTO project)
        {
            var projects = _mapper.Map<Project>(project);
            _context.Add(projects);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> GetById(int Id)
        {
            var projectData = await _context.Projects.FirstOrDefaultAsync(z => z.ProjectId == Id);
            var projects = _mapper.Map<Project>(projectData);
            return projects;
        }

        public async Task<List<ProjectDTO>> GetProjectList()
        {
            var projectList = await _context.Projects.ToListAsync();
            var projects = _mapper.Map<List<ProjectDTO>>(projectList);
            return projects;
        }

        public async Task RemoveProject(int projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(z => z.ProjectId == projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProject(ProjectDTO projectEntity)
        {
            _context.Entry(projectEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
