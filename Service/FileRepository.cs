using WebApi.Database;
using WebApi.Model;

namespace WebApi.Service
{
    public interface IFileRepository
    {
        bool Add(FileOnFileSystemModel model);
    }

    public class FileRepository : IFileRepository
    {
        private ApplicationDbContext _context;
        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(FileOnFileSystemModel model)
        {
            _context.FilesOnFileSystem.Add(model);
            _context.SaveChanges();
            return true;
        }
    }
}
