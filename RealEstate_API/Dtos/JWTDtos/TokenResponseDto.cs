namespace RealEstate_API.Dtos.JWTDtos
{
    public class TokenResponseDto(string token, DateTime expireDate)
    {
        public string Token { get; set; } = token;
        public DateTime ExpireDate { get; set; } = expireDate;
    }
}
