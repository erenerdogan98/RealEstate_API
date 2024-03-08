namespace RealEstate_API.Dtos.JWTDtos
{
    public class GetChecktAppUserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public bool IsExist { get; set; }
    }
}
