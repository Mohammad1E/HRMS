using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.DbContexts;
using HRMS.Dtos.Auth;
using HRMS.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;



namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HRMSContext _dbContext;
        public AuthController(HRMSContext dbContext)
        {
            _dbContext = dbContext;
        }




        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username.ToUpper() == loginDto.Username.ToUpper());
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashedPassword))
            {
                return Unauthorized(new { message = "Invalid username or password" }); //401
            }





            // Generate a JWTbearer token or any other authentication mechanism here
            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        private string GenerateJwtToken(User user)
        {
            // Implement JWT token generation logic here


            //Claims => user info

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); //key-value pair
            claims.Add(new Claim(ClaimTypes.Name, user.Username)); //key-value pair
            //Role => Admin, Hr, Developer, Employee
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            //secret key + signing token 
            //WHAFWEI#!@S!!112312WQEQW@RWQEQW432
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WHAFWEI#!@S!!112312WQEQW@RWQEQW432"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //token setting
            var tokenSetting = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(1)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenSetting);

            return token; // Replace with actual token generation
        }
    }
}
