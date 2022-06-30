using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.Service.ProjectService;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectService _projectService;
        public ProjectController(ApplicationDbContext context, IProjectService projectService)
        {
            _context = context;
            _projectService = projectService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ProjectDTO project)
        {
            if (project == null)
                return BadRequest();
            await _projectService.AddProject(project);
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, ProjectDTO project)
        {
            var entity = _context.Projects.Where(v => v.ProjectId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Project with Id = " + id + " not found");
            await _projectService.UpdateProject(project);
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.Projects.Where(v => v.ProjectId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Project with Id = " + id + " not found");
            await _projectService.RemoveProject(id);
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetProjectList());
        }
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = _context.Projects.Where(v => v.ProjectId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("Project with Id = " + id + " not found");
            return Ok(await _projectService.GetById(id));
        }
    }
}
