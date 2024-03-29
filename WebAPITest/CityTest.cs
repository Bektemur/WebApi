using WebApi.Service.CityService;

namespace WebAPITest
{
    public class CityTest
    {
        private readonly Mock<ICityService> _cityService;
        public CityTest()
        {
            _cityService = new Mock<ICityService>();
        }

        [Fact]
        public async Task AddCity_Test()
        {
            //Arrange
            CityDTO cityDTO = null;
            _cityService.Setup(srvc => srvc.AddCity(It.IsAny<CityDTO>())).Callback<CityDTO>(x=> cityDTO = x);
            var cityData = new CityDTO()
            {
                Name = "Tashkent",
                ProvinceId = 1
            };
            //Act
            await _cityService.Object.AddCity(cityData);

            //Assert
            _cityService.Verify(x=> x.AddCity(It.IsAny<CityDTO>()), Times.Once);
            Assert.Equal(cityDTO.Name, cityData.Name);
            Assert.Equal(cityDTO.ProvinceId, cityData.ProvinceId);
        }

        [Fact]
        public async Task UpdateCity_Test()
        {
            CityDTO cityDTO = null;
            _cityService.Setup(srvc => srvc.UpdateCity(It.IsAny<CityDTO>())).Callback<CityDTO>(x => cityDTO = x);
            var cityData = new CityDTO()
            {
                Name = "Tashkent",
                ProvinceId = 1
            };
            _cityService.Object.UpdateCity(cityData);
            _cityService.Verify(x=>x.UpdateCity(It.IsAny<CityDTO>()), Times.Once);
            Assert.Equal(cityDTO.Name, cityData.Name);
            Assert.Equal(cityDTO.ProvinceId, cityData.ProvinceId);
        }

        [Fact]
        public async Task DeleteCity_Test()
        {
            var cityId = 1;
            _cityService.Setup(srvc => srvc.RemoveCity(cityId));

            await _cityService.Object.RemoveCity(cityId);
            _cityService.Verify(repo => repo.RemoveCity(cityId), Times.Once);
        }
    }
}