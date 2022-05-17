using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApi.Database;
using WebApi.Model;
using WebApi.Service;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        //private readonly ILogger<FileController> _logger;
        public PropertiesController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = applicationDbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
        [HttpPost]
        [Route("AddProperty")]
        public async Task<IActionResult> AddProperties(PropertyMainDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                model.UpdateDate = DateTime.UtcNow;
                var previous = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyId == model.PropertyId);
                if (previous != null)
                {
                    _mapper.Map<PropertyMainDTO, Property>(model, previous);
                    if (string.IsNullOrWhiteSpace(model.ApplicationUserId))
                    {
                        model.ApplicationUserId = GetCurrentUserId();
                    }
                    previous.UpdateDate = DateTime.UtcNow;
                    _context.Properties.Update(previous);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newOne = _mapper.Map<Property>(model);
                    await _context.Properties.AddAsync(newOne);
                    await _context.SaveChangesAsync();
                    model.PropertyId = newOne.PropertyId;
                }
                return Ok(new Response { Status = "Success", Message = "Property Created Successfully" });
            }
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "Property Created Successfully" });
        }

        [HttpPost]
        [Route("AddImprovments")]
        public async Task<IActionResult> Improvments(PropertyImprovmentDTO model)
        {
            //Получение старых улучшений
            var oldImprovments = _context.ImprovementToProperty.Where(x => x.PropertyId == model.PropertyId).ToList();
            //Удаление старых улучшений которые не были выбраны.
            var impToDelete = oldImprovments.Where(x => !model.ImprovmentIds.Contains(x.ImprovementId)).ToList();
            impToDelete.ForEach(x => _context.ImprovementToProperty.Remove(x));
            //Добавление новых которые были добавлены, но не было раньше в БД.
            var newImprovments = model.ImprovmentIds.Where(x => !oldImprovments.Any(y => y.ImprovementId == x)).ToList();
            newImprovments.ForEach(item => _context.ImprovementToProperty.Add(new ImprovementToProperty(model.PropertyId, item)));
            //Итоговое сохранение
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("AddAddress")]
        public async Task<IActionResult> Address(PropertyAddressDTO model)
        {
            if (ModelState.IsValid)
            {
                var previous = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyId == model.PropertyId);
                if (previous != null)
                {
                    _mapper.Map<PropertyAddressDTO, Property>(model, previous);
                    _context.Properties.Update(previous);
                    await _context.SaveChangesAsync();
                    return Ok(new Response { Status = "Success", Message = "Address Created Successfully" });
                }
            }
            return BadRequest("Property with Id = " + model.PropertyId + " not found");
        }

        [HttpPost]
        [Route("AddPhotos")]
        public async Task<IActionResult> Photos([Required]List<IFormFile> files, [Required]int propertyId)
        {
            var previous = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyId == propertyId);
            if (previous != null)
            {
                if (files.Count > 0)
                {
                    if (await UploadToFileSystem(files, propertyId))
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new Response { Status = "Success", Message = "Photos Created Successfully" });
                    }
                }
                return BadRequest("Photos must not be empty");
            }
            return BadRequest("Property with Id = " + propertyId + " not found");
        }

        private async Task<bool> UploadToFileSystem(List<IFormFile> files, int propertyId)
        {
            foreach (var file in files)
            {
                var dateTime = DateTime.UtcNow;
                var internalFolder = Path.Combine("images", "Property", dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString());
                var basePath = Path.Combine(_webHostEnvironment.WebRootPath, internalFolder);
                if (!Directory.Exists(basePath)) { Directory.CreateDirectory(basePath); }
                var fileGuid = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var filePath = Path.Combine(basePath, fileGuid + extension);

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(fileStream);
                    //_logger.LogInformation($"{nameof(UploadToFileSystem)} {filePath} saved");
                }
                _fileService.Add(Path.Combine(internalFolder, fileGuid + extension), file.ContentType, propertyId);
            }
            return true;
            //TempData["Message"] = "File successfully uploaded to File System";
        }
        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpPost]
        [Route("PublishProperty")]
        public async Task<IActionResult> Publish(PropertyDTO model)
        {
            var property = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.PropertyId == model.PropertyId);
            property.PublishingState = model.PublishingState;
            property.Note = model.Note;
            property.UpdateDate = DateTime.UtcNow;
            if (property.PublishingState == PublishingState.Published) { property.PublicDate = DateTime.UtcNow; }
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetPropertyById")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _context.Properties
               .Include(p => p.Project)
               .Include(p => p.Station)
               .Include(p => p.City)
               .Include(p => p.TypeProperties)
               .Include(p => p.Improvements).ThenInclude(x => x.Improvement)
               .Include(p => p.FileSystemModels)
               .Include(p => p.Owner)
               .Include(p => p.Company)
               .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (property == null)
                return BadRequest("Property with Id = " + id + " not found");
            return Ok(property);
        }

        [HttpGet]
        [Route("GetProperties")]
        public IActionResult GetAll()
        {
            return Ok(_context.Properties.ToList());
        }

        [HttpDelete, Route("DeleteProperty")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var properties = await _context.Properties.FindAsync(id);
            if (properties == null)
                return NotFound();
            _context.Properties.Remove(properties);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
