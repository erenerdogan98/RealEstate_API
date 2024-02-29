namespace RealEstate_API.Dtos.TestimonialDtos
{
    public class ResultTestimonialDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public bool Status { get; set; } = false;
    }
}
