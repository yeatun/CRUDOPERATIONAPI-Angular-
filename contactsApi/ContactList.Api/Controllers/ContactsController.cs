using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MediatR;
using ContactList.Application.Queries.Contacts;
using ContactList.Application.Shared.Commands.Contacts;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //private readonly ApplicationDbContext _context;

        //public ContactsController(ApplicationDbContext context) 
        //{
        //    _context = context;
        //}
        // GET: api/<ContactsController>]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAllContactQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            return Ok(await _mediator.Send(new GetContactByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactCommand command)
        {

            if (command.Id == id)
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {

            string response = string.Empty;
            response = await _mediator.Send(new DeleteContactCommand(id));
            return Ok(response);
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
