using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController(IStatisticsRepository statisticsRepository) : ControllerBase
    {
        // this statistics controller for get some values to show for report 
        // logging will add.
        private readonly IStatisticsRepository _statisticsRepository = statisticsRepository ?? throw new ArgumentNullException(nameof(statisticsRepository));

        [HttpGet("ActiveCategoryCount")]
        public async Task<IActionResult> GetActiveCategoryCountAsync()
        {
            var activeCategories = await _statisticsRepository.ActiveCategoryCountAsync();
            return activeCategories != 0 ? Ok(activeCategories) : NoContent();
        }

        [HttpGet("ActiveEmployeeCount")]
        public async Task<IActionResult> GetActiveEmployeeCountAsync()
        {
            var activeEmployees = await _statisticsRepository.ActiveEmployeeCountAsync();
            return activeEmployees != 0 ? Ok(activeEmployees) : NoContent();
        }

        [HttpGet("ApartmentCount")]
        public async Task<IActionResult> GetApartmentCountAsync()
        {
            var apartmentsC = await _statisticsRepository.ApartmentCountAsync();
            return apartmentsC != 0 ? Ok(apartmentsC) : NoContent();
        }

        [HttpGet("AveragePriceRented")]
        public async Task<IActionResult> GetAveragePriceByRentAsync()
        {
            var priceRentedAv = await _statisticsRepository.AverageProductPriceByRentAsync();
            return priceRentedAv != 0 ? Ok(priceRentedAv) : NoContent();
        }

        [HttpGet("AveragePriceSaled")]
        public async Task<IActionResult> GetAveragePriceBySaleAsync()
        {
            var priceSaledAv = await _statisticsRepository.AverageProductPriceBySaleAsync();
            return priceSaledAv != 0 ? Ok(priceSaledAv) : NoContent();
        }

        [HttpGet("AverageRoomCount")]
        public async Task<IActionResult> GetAverageRoomCountAsync()
        {
            var roomsCountAve = await _statisticsRepository.AverageRoomCountAsync();
            return roomsCountAve != 0 ? Ok(roomsCountAve) : NoContent();
        }

        [HttpGet("CategoryCounts")]
        public async Task<IActionResult> GetCategoryCountAsync()
        {
            var categoryCount= await _statisticsRepository.CategoryCountAsync();
            return categoryCount != 0 ? Ok(categoryCount) : NoContent();
        }
        [HttpGet("CategoryByMaxProduct")]
        public async Task<IActionResult> GetCategoryNameByMaxProductCountAsync()
        {
            var categoryNameCount = await _statisticsRepository.CategoryNameByMaxProductCountAsync();
            return categoryNameCount != null ? Ok(categoryNameCount) : NoContent();
        }

        [HttpGet("CityByMaxProduct")]
        public async Task<IActionResult> GetCityNameByMaxProductCountAsync()
        {
            var cityNameCount = await _statisticsRepository.CityNameByMaxProductCountAsync();
            return cityNameCount != null ? Ok(cityNameCount) : NoContent();
        }

        [HttpGet("DifferentCityCounts")]
        public async Task<IActionResult> GetDifferentCityCountAsync()
        {
            var diffCityCount = await _statisticsRepository.DifferentCityCountAsync();
            return diffCityCount != 0 ? Ok(diffCityCount) : NoContent();
        }

        [HttpGet("EmployeeByMaxProduct")]
        public async Task<IActionResult> GetEmployeeNameByMaxProductCountAsync()
        {
            var emplNameCount = await _statisticsRepository.EmployeeNameByMaxProductCountAsync();
            return emplNameCount != null ? Ok(emplNameCount) : NoContent();
        }

        [HttpGet("LastPrice")]
        public async Task<IActionResult> GetLastPriceProductAsync()
        {
            var lastPriceP = await _statisticsRepository.LastPriceProductAsync();
            return lastPriceP != 0 ? Ok(lastPriceP) : NoContent();
        }

        [HttpGet("NewestBuilding")]
        public async Task<IActionResult> GetNewestBuildingAsync()
        {
            var newestBuilding = await _statisticsRepository.NewestBuildingAsync();
            return newestBuilding != null ? Ok(newestBuilding) : NoContent();
        }

        [HttpGet("OldestBuilding")]
        public async Task<IActionResult> GetOldestBuildingAsync()
        {
            var oldestBuilding = await _statisticsRepository.OldestBuildingAsync();
            return oldestBuilding != null ? Ok(oldestBuilding) : NoContent();
        }

        [HttpGet("PassiveCategoryCount")]
        public async Task<IActionResult> GetPassiveCategoryCountAsync()
        {
            var passCategoryC = await _statisticsRepository.CategoryCountAsync();
            return passCategoryC != 0 ? Ok(passCategoryC) : NoContent();
        }

        [HttpGet("ProductCount")]
        public async Task<IActionResult> GetProductCountAsync()
        {
            var productC = await _statisticsRepository.CategoryCountAsync();
            return productC != 0 ? Ok(productC) : NoContent();
        }
    }
}
