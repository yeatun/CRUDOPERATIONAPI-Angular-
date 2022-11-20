using ContactList.Api.helper;
using ContactList.Core.Entities;
using ContactList.Infrastructure.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _authcontext;

        public UserController(ApplicationDbContext context)
        {
            _authcontext = context;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if(userObj == null)
                return BadRequest();


            var user = await _authcontext.Users
                .FirstOrDefaultAsync(x => x.Username == userObj.Username);
            if (user == null)
                return NotFound(new {Message = "User Not Found"});

            var hashpass = PasswordHasher.HashPassword(userObj.Password);

            if(PasswordHasher.VerifyPassword(userObj.Password, hashpass))
            {
                return Ok(new
                {
                    Message = "Login Successful"
                });
            }
            return NotFound(new { Message = "Wrong Password" });

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Token = "";
            await _authcontext.Users.AddAsync(userObj);
            await _authcontext.SaveChangesAsync();
            return Ok(new
            {
                Message = "Registration Successful"
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAsync()
        {
            if (_authcontext.Users == null)
            {
                return NotFound();
            }
            var users = await _authcontext.Users.ToListAsync();
            return Ok(users);
        }
    }
}
