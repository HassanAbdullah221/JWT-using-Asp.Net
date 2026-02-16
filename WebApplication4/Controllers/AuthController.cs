using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || user.Password != dto.Password)
                return Unauthorized("Invalid credentials");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_config["Jwt:DurationInMinutes"])
                ),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        //    [AllowAnonymous]
        //    [HttpPost("register")]
        //    public async Task<IActionResult> Register(RegisterDto dto)
        //    {
        //        var existingUser = await _db.Users
        //            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        //        if (existingUser != null)
        //            return BadRequest("Email already exists");

        //        var user = new User
        //        {
        //            Id = Guid.NewGuid(),
        //            FullName = dto.FullName,
        //            Email = dto.Email,
        //            Department = dto.Department,
        //            PhoneNumber = dto.PhoneNumber,
        //            Password = dto.Password,
        //            Role = "User"
        //        };

        //        _db.Users.Add(user);
        //        await _db.SaveChangesAsync();

        //        return Ok("User registered successfully");
        //    }

    }
}
