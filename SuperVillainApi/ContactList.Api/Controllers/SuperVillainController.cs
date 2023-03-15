using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ContactList.Core.Entities;
using ContactList.Application.Queries.VillainQuery;
using ContactList.Application.Commands.Villain.Create;
using ContactList.Application.Commands.Villain.Update;
using ContactList.Application.Commands.Villain.Delete;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SuperVillainController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuperVillainController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //private readonly ApplicationDbContext _context;

        //public ContactsController(ApplicationDbContext context) 
        //{
        //    _context = context;
        //}
        // GET: api/<ContactsController>]
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<SuperVillain>> Get()
        {
            return await _mediator.Send(new GetAllSuperVillainQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<SuperVillain> Get( Int64 id)
        {
            return await _mediator.Send(new GetSuperVillainByIdQuery(id));
        }
       
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSuperVillain([FromBody] CreateSuperVillainCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] EditSuperVillainCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSuperVillain(int id)
        {

            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeleteSuperVillainCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        // GET api/<ContactsController>/5
        //        [HttpGet]
        //        [Route("{id:guid}")]
        //        public async Task<ActionResult<IEnumerable<Contact>>> GetContact([FromRoute] Guid id)
        //        {
        //            var contacts = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        //            if (contacts != null) 
        //            {
        //                return Ok(contacts);
        //            }
        //            return NotFound("Contact not found");
        //        }

        //        // POST api/<ContactsController>
        //        [HttpPost]
        //        public async Task<IActionResult> AddContact([FromBody] Contact contact)
        //        {
        //           // contact.Id = Guid.NewGuid();
        //            await _context.AddAsync(contact);
        //            await _context.SaveChangesAsync();
        //            return CreatedAtAction(nameof(GetContact), new { id= contact.Id }, contact);
        //        }

        //        // PUT api/<ContactsController>/5
        //        [HttpPut]
        //        [Route("{id:guid}")]
        //        public async Task<ActionResult<IEnumerable<Contact>>> UpdateContact([FromRoute] Guid id, [FromBody] Contact contact)
        //        {
        //            var existingcontact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        //            if(existingcontact != null)
        //            {
        //                existingcontact.FirstName = contact.FirstName;
        //                existingcontact.LastName = contact.LastName;
        //                existingcontact.Email = contact.Email;
        //                existingcontact.PhoneNumber = contact.PhoneNumber;
        //                existingcontact.Company = contact.Company;
        //                await _context.SaveChangesAsync();
        //                return Ok(existingcontact);
        //            }
        //            return NotFound("Contact not found");
        //        }

        //        // DELETE api/<ContactsController>/5
        //        [HttpDelete]
        //        [Route("{id:guid}")]
        //        public async Task<ActionResult<IEnumerable<Contact>>> DeleteContact([FromRoute] Guid id)
        //        {
        //            var existingcontact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        //            if (existingcontact != null)
        //            {
        //                _context.Remove(existingcontact);
        //                await _context.SaveChangesAsync();
        //                return Ok(existingcontact);
        //            }
        //            return NotFound("Contact not found");
        //        }
    }
}
