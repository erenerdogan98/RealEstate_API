using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using RealEstate_UI.Dtos.JwtDtos;
using RealEstate_UI.Dtos.LoginDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RealEstate_UI.Controllers
{
    [Route("{controller}")]
    public class LoginController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly string dataType = "application/json";

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Index")]
        public async Task<IActionResult> Index(CreateLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonSerializer.Serialize(loginDto);
                StringContent stringContent = new(jsonData, Encoding.UTF8, dataType);

                var responseMessage = await client.PostAsync("https://localhost:44349/api/Login/SignIn", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readData = await responseMessage.Content.ReadAsStreamAsync();
                    JsonSerializerOptions options = new()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var tokenModel = JsonSerializer.Deserialize<JwtResponseDto>(readData, options);
                    if(tokenModel != null)
                    {
                        var token = tokenModel.Token;
                        JwtSecurityTokenHandler handler = new();
                        var securityToken = handler.ReadJwtToken(token);
                        var claims = securityToken.Claims.ToList();
                        if (token != null)
                        {
                            claims.Add(new Claim("realestatetoken", token));
                            var claimsIdentity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);
                            var authProps = new AuthenticationProperties
                            {
                                ExpiresUtc = tokenModel.ExpireDate,
                                IsPersistent = true
                            };
                            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProps);
                            return RedirectToAction("Index","Employee"); // area olduğu için doğru yolu bul 
                        }
                    }
                }

                var errorMessage = $"Failed to create Employee. Status code: {responseMessage.StatusCode}.";
                return Content(errorMessage);

            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message later will use Serilog 
                var errorMessage = $"An error occurred: {ex.Message}";
                return Content(errorMessage);
            }
        }
    }
}
