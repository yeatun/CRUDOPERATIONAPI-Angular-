using CRUDOPERATIONAPI.Context;
using CRUDOPERATIONAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDOPERATIONAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperVillainController : ControllerBase
    {
        private readonly CrudOperationReactContext _reactContext;
        public SuperVillainController(CrudOperationReactContext reactContext)
        {
            _reactContext = reactContext;
        }

        //this for Auth API
        //Login

        [HttpPost("authenticate")]

        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
           if (userObj == null)
              return BadRequest();

            var user = await _reactContext.Users.FirstOrDefaultAsync(x=>x.Username == userObj.Username && x.Password == userObj.Password);

            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                Message ="Login Success"
            });



        }
        //Register

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            /*if(string.IsNullOrEmpty(userObj.Username))*/

            await _reactContext.Users.AddAsync(userObj);
            await _reactContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered"
            });
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var villains = await _reactContext.SuperVillain.ToListAsync();
            return Ok(villains);
        }
        [HttpPost]
        public async Task<IActionResult> Post(SuperVillain newSuperVillain)
        {
            _reactContext.SuperVillain.Add(newSuperVillain);
            await _reactContext.SaveChangesAsync();
            return Ok(newSuperVillain);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var villainById = await _reactContext.SuperVillain.FindAsync(id);
            return Ok(villainById);
        }
        [HttpPut]
        public async Task<IActionResult> Put(SuperVillain superVillainToUpdate)
        {
            _reactContext.SuperVillain.Update(superVillainToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(superVillainToUpdate);

        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var villainToDelete = await _reactContext.SuperVillain.FindAsync(id);
            if (villainToDelete == null)
            {
                return NotFound();
            }
            _reactContext.SuperVillain.Remove(villainToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();
        }

    }
}
