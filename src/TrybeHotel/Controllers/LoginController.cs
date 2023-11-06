using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("login")]

    public class LoginController : Controller
    {

        private readonly IUserRepository _repository;
        private readonly TokenGenerator _tokenGenerator;


        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]

        public IActionResult Login([FromBody] LoginDto login)
        {
            UserDto user = _repository.Login(login);

            if (user == null)
            {
                return StatusCode(401, new { message = "Incorrect e-mail or password" });
            }

            String token = new TokenGenerator().Generate(user);

            return Ok(new { token });


        }
    }
}