using LoginRegister.Database;
using LoginRegister.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LoginRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LoginDbContext dbContext;

        public UsersController(LoginDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);

            if (user == null)
            {
                dbContext.Users.Add(new Models.User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Password = userDTO.Password,
                    Email = userDTO.Email
                });

                dbContext.SaveChanges();
                return Ok(new { Message = "User registered successfully" });
            }
            else
            {
                return BadRequest("User already exists with the same email address");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);

            if (user != null)
            {
                return Ok(user);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);

            if (user != null)
                return Ok(user);
            else
                return BadRequest("User not found");
        }
    }
}
