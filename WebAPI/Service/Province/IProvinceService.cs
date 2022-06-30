using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Service.ProvinceService
{
    public interface IProvinceService
    {
        Task AddProvince(ProvinceDTO provinceEntity);
        Task<List<ProvinceDTO>> GetProvinceList();
        Task<Province> GetById(int provinceId);
        Task UpdateProvince(ProvinceDTO provinceEntity);
        Task RemoveProvince(int provinceId);
    }
}
