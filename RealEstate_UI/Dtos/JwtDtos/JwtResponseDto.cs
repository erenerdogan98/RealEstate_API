namespace RealEstate_UI.Dtos.JwtDtos
{
    public class JwtResponseDto
    {
        public string? Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
