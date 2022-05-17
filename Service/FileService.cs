using WebApi.Model;

namespace WebApi.Service
{
    public interface IFileService
    {
        bool Add(string fileName, string contentType, int propertyid);
    }

    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPropertyService _propertyService;

        public FileService(IFileRepository repository,
            IWebHostEnvironment webHostEnvironment,
            IPropertyService propertyService)
        {
            _webHostEnvironment = webHostEnvironment;
            _repository = repository;
            _propertyService = propertyService;
        }

        public bool Add(string filePath, string contentType, int propertyid)
        {
            string propertyName = _propertyService.GetPrypertyNameById(propertyid);

            var fileModel = new FileOnFileSystemModel
            {
                CreatedOn = DateTime.UtcNow,
                FileType = contentType,
                Extension = Path.GetExtension(filePath),
                Name = Path.GetFileName(filePath),
                PropertyId = propertyid,
                PropertyName = propertyName,
                FilePath = filePath
            };
            return _repository.Add(fileModel);
        }
    }
}
