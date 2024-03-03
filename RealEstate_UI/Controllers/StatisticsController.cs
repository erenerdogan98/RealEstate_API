using Microsoft.AspNetCore.Mvc;
using RealEstate_UI.ViewModels;

namespace RealEstate_UI.Controllers
{
    public class StatisticsController(IHttpClientFactory httpClientFactory) : Controller
    {
        /* Normally, we could do it using ViewBag, but this is not suitable for SOLID and is a bad method in terms of readability,
         so I created a class called StatisticViewModel and defined the relevant statistics as properties. */
        // But I still leave it as a comment line to show how to move it with ViewBag.
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private const string GeneralErrorMessage = "An error occurred while fetching statistics. Please try again later.";
        public async Task<IActionResult> Index()
        {
            //await GetStatistic("ActiveCategoryCount", nameof(ViewBag.activeCategoryCount));
            //await GetStatistic("ActiveEmployeeCount", nameof(ViewBag.activeEmployeeCount));
            //await GetStatistic("ApartmentCount", nameof(ViewBag.apartmentCount));
            //await GetStatistic("AveragePriceRented", nameof(ViewBag.averagePriceRented));
            //await GetStatistic("AveragePriceSaled", nameof(ViewBag.averagePriceSaled));
            //await GetStatistic("AverageRoomCount", nameof(ViewBag.averageRoomCount));
            //await GetStatistic("CategoryCounts", nameof(ViewBag.categoryCounts));
            //await GetStatistic("CategoryByMaxProduct", nameof(ViewBag.categoryByMaxProduct));
            //await GetStatistic("CityByMaxProduct", nameof(ViewBag.cityByMaxProduct));
            //await GetStatistic("DifferentCityCounts", nameof(ViewBag.differentCityCounts));
            //await GetStatistic("EmployeeByMaxProduct", nameof(ViewBag.employeeByMaxProduct));
            //await GetStatistic("LastPrice", nameof(ViewBag.lastPrice));
            //await GetStatistic("NewestBuilding", nameof(ViewBag.newestBuilding));
            //await GetStatistic("OldestBuilding", nameof(ViewBag.oldestBuilding));
            //await GetStatistic("PassiveCategoryCount", nameof(ViewBag.passiveCategoryCount));
            //await GetStatistic("ProductCount", nameof(ViewBag.productCount));

            var viewModel = new StatisticsViewModel(); 

            await GetStatistic("ActiveCategoryCount", x => viewModel.ActiveCategoryCount = ParseInt(x));
            await GetStatistic("ActiveEmployeeCount", x => viewModel.ActiveEmployeeCount = ParseInt(x));
            await GetStatistic("ApartmentCount", x => viewModel.ApartmentCount = ParseInt(x));
            await GetStatistic("AveragePriceRented", x => viewModel.AveragePriceRented = ParseDecimal(x));
            await GetStatistic("AveragePriceSaled", x => viewModel.AveragePriceSaled = ParseDecimal(x));
            await GetStatistic("AverageRoomCount", x => viewModel.AverageRoomCount = ParseInt(x));
            await GetStatistic("CategoryCounts", x => viewModel.CategoryCounts = ParseInt(x));
            await GetStatistic("CategoryByMaxProduct", x => viewModel.CategoryNameByMaxProduct = x);
            await GetStatistic("CityByMaxProduct", x => viewModel.CityNameByMaxProduct = x);
            await GetStatistic("DifferentCityCounts", x => viewModel.DifferentCityCounts = ParseInt(x));
            await GetStatistic("EmployeeByMaxProduct", x => viewModel.EmployeeNameByMaxProduct = x);
            await GetStatistic("LastPrice", x => viewModel.LastPrice = ParseInt(x));
            await GetStatistic("NewestBuilding", x => viewModel.NewestBuilding = x);
            await GetStatistic("OldestBuilding", x => viewModel.OldestBuilding = x);
            await GetStatistic("PassiveCategoryCount", x => viewModel.PassiveCategoryCount = ParseInt(x));
            await GetStatistic("ProductCount", x => viewModel.ProductCount = ParseInt(x));

            return View(viewModel); 
        }

        private async Task GetStatistic(string apiEndpoint, Action<string> setProperty)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44349/api/Statistics/{apiEndpoint}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                setProperty(jsonData);
            }
            ModelState.AddModelError("", GeneralErrorMessage);
        }

        private static int ParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        private static decimal ParseDecimal(string value)
        {
            return decimal.TryParse(value, out var result) ? result : 0;
        }
    }
}
