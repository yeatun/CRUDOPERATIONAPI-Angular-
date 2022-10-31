using ContactList.Infrastructure.Data;
using ContactList.Core.Entities;
using ContactList.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context) 
        {
            _context = context;
        }
        // GET: api/<ContactsController>
        [EnableCors()]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAsync()
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contacts = await _context.Contacts.ToListAsync() ;
            return Ok(contacts);
        }

        // GET api/<ContactsController>/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact([FromRoute] Guid id)
        {
            var contacts = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contacts != null) 
            {
                return Ok(contacts);
            }
            return NotFound("Contact not found");
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] Contact contact)
        {
            contact.Id = Guid.NewGuid();
            await _context.AddAsync(contact);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetContact), new { id= contact.Id }, contact);
        }

        // PUT api/<ContactsController>/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<IEnumerable<Contact>>> UpdateContact([FromRoute] Guid id, [FromBody] Contact contact)
        {
            var existingcontact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if(existingcontact != null)
            {
                existingcontact.FirstName = contact.FirstName;
                existingcontact.LastName = contact.LastName;
                existingcontact.Email = contact.Email;
                existingcontact.PhoneNumber = contact.PhoneNumber;
                existingcontact.Company = contact.Company;
                await _context.SaveChangesAsync();
                return Ok(existingcontact);
            }
            return NotFound("Contact not found");
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<IEnumerable<Contact>>> DeleteContact([FromRoute] Guid id)
        {
            var existingcontact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingcontact != null)
            {
                _context.Remove(existingcontact);
                await _context.SaveChangesAsync();
                return Ok(existingcontact);
            }
            return NotFound("Contact not found");
        }
    }
}
