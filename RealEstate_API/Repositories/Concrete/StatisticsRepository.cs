using Dapper;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Repositories.Concrete
{
    public class StatisticsRepository(Context context) : IStatisticsRepository
    {
        private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<int> ActiveCategoryCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Category WHERE Status=1"; // status equal 1 mean, it is true .
            using var connection = _context.GetConnection();
            var activeCategoriesC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return activeCategoriesC;
        }

        public async Task<int> ActiveEmployeeCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Employee WHERE Status=1"; 
            using var connection = _context.GetConnection();
            var activeEmployeesC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return activeEmployeesC;
        }

        public async Task<int> ApartmentCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Product WHERE Title LIKE '%Apartment%'";
            using var connection = _context.GetConnection();
            var apartmentC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return apartmentC;
        }

        public async Task<decimal> AverageProductPriceByRentAsync()
        {
            string query = "SELECT AVG(Price) FROM Product WHERE Type='Rented'";
            using var connection = _context.GetConnection();
            var priceSaledA = await connection.QueryFirstOrDefaultAsync<int>(query);
            return priceSaledA;
        }

        public async Task<decimal> AverageProductPriceBySaleAsync()
        {
            string query = "SELECT AVG(Price) FROM Product WHERE Type='Saled'";
            using var connection = _context.GetConnection();
            var priceRentedA = await connection.QueryFirstOrDefaultAsync<int>(query);
            return priceRentedA;
        }

        public async Task<int> AverageRoomCountAsync()
        {
            string query = "SELECT AVG(RoomCount) FROM ProductDetails";
            using var connection = _context.GetConnection();
            var roomCountA = await connection.QueryFirstOrDefaultAsync<int>(query);
            return roomCountA;
        }

        public async Task<int> CategoryCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Category";
            using var connection = _context.GetConnection();
            var categoryC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return categoryC;
        }

        public async Task<string> CategoryNameByMaxProductCountAsync()
        {
            string query = "SELECT TOP(1) Name,Count(*) FROM Product JOIN Category ON Product.CategoryId=Category.Id GROUP BY Name ORDER BY COUNT(*) DESC";
            using var connection = _context.GetConnection();
            var categoryC = await connection.QueryFirstOrDefaultAsync<string>(query);
            return categoryC ?? throw new Exception("Category Name not found!");
        }

        public async Task<string> CityNameByMaxProductCountAsync()
        {
            string query = "SELECT TOP(1) City,COUNT(*) AS 'product_count' FROM Product GROUP BY City ORDER BY product_count DESC";
            using var connection = _context.GetConnection();
            var cityNameC = await connection.QueryFirstOrDefaultAsync<string>(query);
            return cityNameC ?? throw new ArgumentNullException(nameof(cityNameC));
        }

        public async Task<int> DifferentCityCountAsync()
        {
            string query = "SELECT COUNT(Distinct(City)) FROM Product";
            using var connection = _context.GetConnection();
            var difCityNameC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return difCityNameC;
        }

        public async Task<string> EmployeeNameByMaxProductCountAsync()
        {
            string query = "SELECT TOP(1) Employee.Name, COUNT(*) AS 'product_count' FROM Product JOIN Employee ON Product.EmployeeId = Employee.Id GROUP BY Employee.Name ORDER BY product_count DESC";
            using var connection = _context.GetConnection();
            var employeeName = await connection.QueryFirstOrDefaultAsync<string>(query);
            return employeeName ?? throw new ArgumentNullException(nameof(employeeName));
        }

        public async Task<int> LastPriceProductAsync()
        {
            string query = "SELECT TOP(1) Price FROM Product ORDER BY Id DESC";
            using var connection = _context.GetConnection();
            var lastPriceP = await connection.QueryFirstOrDefaultAsync<int>(query);
            return lastPriceP;
        }

        public async Task<string> NewestBuildingAsync()
        {
            string query = "SELECT TOP(1) BuildYear FROM ProductDetails ORDER BY BuildYear ASC";
            using var connection = _context.GetConnection();
            var newestBuildYear = await connection.QueryFirstOrDefaultAsync<string>(query);
            return newestBuildYear ?? throw new ArgumentNullException(nameof(newestBuildYear));
        }

        public async Task<string> OldestBuildingAsync()
        {
            string query = "SELECT TOP(1) BuildYear FROM ProductDetails ORDER BY BuildYear DESC";
            using var connection = _context.GetConnection();
            var oldestBuildYear = await connection.QueryFirstOrDefaultAsync<string>(query);
            return oldestBuildYear ?? throw new ArgumentNullException(nameof(oldestBuildYear));
        }

        public async Task<int> PassiveCategoryCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Category WHERE Status=0";
            using var connection = _context.GetConnection();
            var passiveCategoryC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return passiveCategoryC;
        }

        public async Task<int> ProductCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Product";
            using var connection = _context.GetConnection();
            var productC = await connection.QueryFirstOrDefaultAsync<int>(query);
            return productC;
        }
    }
}
