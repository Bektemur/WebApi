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
        [Fact]
        public async Task GetAllCity_Test()
        {
            _cityService.Setup(srvc => srvc.GetCityList()).ReturnsAsync(new List<CityDTO>()
            {
                new CityDTO() { ProvinceId = 1, Name = "Tashkent" },
                new CityDTO() { ProvinceId = 2, Name = "Fergana" },
                new CityDTO() { ProvinceId = 2, Name = "Samarqand" },
            });
            var result = await _cityService.Object.GetCityList();
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task GetCity_Test()
        {
            int cityId = 1;
            _cityService.Setup(srvc => srvc.GetById(cityId)).ReturnsAsync(new CityDTO() { Name = "Tashkent", ProvinceId = 1 });
            var result = await _cityService.Object.GetById(cityId);
            Assert.NotNull(result);
        }
    }
}