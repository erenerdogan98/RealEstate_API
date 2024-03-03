namespace RealEstate_UI.ViewModels
{
    public class StatisticsViewModel
    {
        public int ActiveCategoryCount { get; set; }
        public int ActiveEmployeeCount { get; set; }
        public int ApartmentCount { get; set; }
        public decimal AveragePriceRented { get; set; }
        public decimal AveragePriceSaled { get; set; }
        public int AverageRoomCount { get; set; }
        public int CategoryCounts { get; set; }
        public string? CategoryNameByMaxProduct { get; set; }
        public string? CityNameByMaxProduct { get; set; }
        public int DifferentCityCounts { get; set; }
        public string? EmployeeNameByMaxProduct { get; set; }
        public int LastPrice { get; set; }
        public string? NewestBuilding { get; set; }
        public string? OldestBuilding { get; set; }
        public int PassiveCategoryCount { get; set; }
        public int ProductCount { get; set; }
    }
}
