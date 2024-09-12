using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebAPI.Data;
using SchoolManagementWebAPI.Models;

namespace SchoolManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp model)
        {
             if (await db.Users.AnyAsync(u => u.Username == model.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Role = model.Role ?? "User"
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [Route("signIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn model)
        {
            var user = await db.Users
                .SingleOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new { Username = user.Username, Role = user.Role });
        }
    }
}
