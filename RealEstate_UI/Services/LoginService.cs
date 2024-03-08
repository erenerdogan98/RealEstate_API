using System.Security.Claims;

namespace RealEstate_UI.Services
{
    public class LoginService(IHttpContextAccessor httpContextAccessor) : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        public string GetCurrentUserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? throw new InvalidDataException();
    }
}
