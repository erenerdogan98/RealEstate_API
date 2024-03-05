using Microsoft.AspNetCore.Mvc;
using RealEstate_UI.ViewModels;

namespace RealEstate_UI.ViewComponents.Dashboard
{
    public class DashboardStatisticComponentPartial(IHttpClientFactory httpClientFactory) : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private const string GeneralErrorMessage = "An error occurred while fetching statistics. Please try again later.";
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new StatisticsViewModel();
            await GetStatistic("ProductCount", x => viewModel.ProductCount = ParseInt(x));
            await GetStatistic("EmployeeByMaxProduct", x => viewModel.EmployeeNameByMaxProduct = x);
            await GetStatistic("DifferentCityCounts", x => viewModel.DifferentCityCounts = ParseInt(x));
            await GetStatistic("AveragePriceRented", x => viewModel.AveragePriceRented = ParseDecimal(x));
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
