namespace RealEstate_UI.Dtos.ProductDtos
{
    public class ResultLastFiveProductsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Name { get; set; }
        public DateTime AdvertisementDate { get; set; }
    }
}
