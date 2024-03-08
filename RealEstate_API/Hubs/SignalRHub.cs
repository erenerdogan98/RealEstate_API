using Microsoft.AspNetCore.SignalR;

namespace RealEstate_API.Hubs
{
    public class SignalRHub(IHttpClientFactory httpClientFactory, ILogger<SignalRHub> logger) : Hub
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentException(nameof(httpClientFactory));
        private readonly ILogger<SignalRHub> _logger = logger;

        public async Task SendCategoryCount()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiEndpoint = "CategoryCounts";
                var responseMessage = await client.GetAsync($"https://localhost:44349/api/Statistics/{apiEndpoint}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    await Clients.All.SendAsync("ReceiveCategoryCount", jsonData);
                }
                else
                {
                    _logger.LogError($"Failed to get data. Status Code: {responseMessage.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }

        }

    }
}
