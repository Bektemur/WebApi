using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProjectController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(ProjectDTO project)
        {
            if (project == null)
                return BadRequest();
            var projects = _mapper.Map<Project>(project);
            _context.Projects.Add(projects);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Update(int id, ProjectDTO project)
        {
            var entity = _context.Projects.Where(v => v.ProjectId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            entity.Name = project.Name;
            entity.CreatedDate = DateTime.Now;
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _context.Projects.Where(v => v.ProjectId == id).FirstOrDefault();
            if (entity == null)
                return BadRequest("City with Id = " + id + " not found");
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_context.Projects.ToList());
        }
    }
}
