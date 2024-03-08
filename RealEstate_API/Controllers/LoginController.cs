using Dapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.JWTDtos;
using RealEstate_API.Dtos.LoginDtos;
using RealEstate_API.Models.DapperContext;
using RealEstate_API.Repositories.Abstract;
using RealEstate_API.Tools;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(Context context) : ControllerBase
    {
        private readonly Context _context = context;
        private readonly DynamicParameters parameters = new();


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(CreateLoginDto createLoginDto)
        {
            try
            {
                if (createLoginDto != null)
                {
                    string userName = createLoginDto.UserName ?? throw new InvalidDataException();
                    string password = createLoginDto.Password ?? throw new Exception();

                    string queryLogin = "SELECT * FROM AppUser WHERE UserName=@userName and Password=@password";

                    //string queryUserId = "SELECT Id FROM AppUser WHERE UserName=@userName and Password=@password";
                    parameters.Add("@userName", userName);
                    parameters.Add("@password", password);
                    using var connection = _context.GetConnection();

                    var user = await connection.QueryFirstOrDefaultAsync(queryLogin, parameters);
                    if (user != null)
                    {
                        int id = user.Id;
                        GetChecktAppUserDto model = new()
                        {
                            UserName = userName,
                            Id = id
                        };
                        var token = JwtTokenGenerator.GenerateToken(model);
                        return Ok(token);
                    }
                  
                }
                return BadRequest("");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error : {ex.Message}");
            }

        }
    }
}
