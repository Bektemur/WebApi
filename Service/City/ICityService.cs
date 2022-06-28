﻿using WebApi.ViewModel;

namespace WebApi.Service.CityService
{
    public interface ICityService
    {
        Task AddCity(CityDTO cityEntity);
        Task<List<CityDTO>> GetCityList();
        Task<CityDTO> GetById(int cityId);
        Task UpdateCity(CityDTO cityEntity);
        Task RemoveCity(int cityId);
    }
}
