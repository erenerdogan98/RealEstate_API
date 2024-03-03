namespace RealEstate_API.Repositories.Abstract
{
    public interface IStatisticsRepository
    {
        // I define these methods to retrieve some data from the Database for statistics.
        Task<int> CategoryCountAsync();
        Task<int> ActiveCategoryCountAsync();
        Task<int> PassiveCategoryCountAsync();
        Task<int> ProductCountAsync();
        Task<int> ApartmentCountAsync();
        Task<string> EmployeeNameByMaxProductCountAsync();
        Task<string> CategoryNameByMaxProductCountAsync();
        Task<decimal> AverageProductPriceByRentAsync();
        Task<decimal> AverageProductPriceBySaleAsync();
        Task<string> CityNameByMaxProductCountAsync();
        Task<int> DifferentCityCountAsync();
        Task<int> LastPriceProductAsync();
        Task<string> NewestBuildingAsync();
        Task<string> OldestBuildingAsync();
        Task<int> ActiveEmployeeCountAsync();
        Task<int> AverageRoomCountAsync();
        
    }
}
