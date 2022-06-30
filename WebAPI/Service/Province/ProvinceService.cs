using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Service.ProvinceService
{
    public class ProvinceService : IProvinceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProvinceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProvince(ProvinceDTO province)
        {
            var provinces = _mapper.Map<Province>(province);
            _context.Add(provinces);
            await _context.SaveChangesAsync();
        }

        public async Task<Province> GetById(int provinceId)
        {
            var provinceData = await _context.Province.FirstOrDefaultAsync(z => z.ProvinceId == provinceId);
            var provinces = _mapper.Map<Province>(provinceData);
            return provinces;
        }

        public async Task<List<ProvinceDTO>> GetProvinceList()
        {
            var provinceList = await _context.Province.ToListAsync();
            var provinces = _mapper.Map<List<ProvinceDTO>>(provinceList);
            return provinces;
        }

        public async Task RemoveProvince(int provinceId)
        {
            var province = await _context.Province.FirstOrDefaultAsync(z => z.ProvinceId == provinceId);
            if (province != null)
            {
                _context.Province.Remove(province);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProvince(ProvinceDTO province)
        {
            _context.Entry(province).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
