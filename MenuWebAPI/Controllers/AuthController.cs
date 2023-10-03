using MenuWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MenuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> userRegister([FromBody] User user) 
        {

            try
            {
                var dbUser = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();

                if (dbUser != null)
                {
                    return BadRequest("User is already exists");
                }

                user.Password = hashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                //bunu sonradan ekledim
                var newdbUser = _context.Users.Where(u => u.Id == user.Id).Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                }).FirstOrDefault();


                return Ok(newdbUser);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> userLogin([FromBody] User user)
        {
            try
            {
                string password = hashPassword(user.Password);
                var dbUser = _context.Users.Where(u => u.Email == user.Email && u.Password == password).Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                }).FirstOrDefault();

                if (dbUser == null)
                {
                    return BadRequest("Username or password is wrong");
                }

                var token = CreateToken(user);

                return Ok(token);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        
        }
       

        //pasword hashed
        public static string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);  
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
