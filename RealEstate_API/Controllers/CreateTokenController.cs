using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.JWTDtos;
using RealEstate_API.Tools;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateToken(GetChecktAppUserDto model)
        {
            var jwtToken = JwtTokenGenerator.GenerateToken(model) ?? throw new Exception("Token can not generate");
            return Ok(jwtToken);
        }
    }
}
