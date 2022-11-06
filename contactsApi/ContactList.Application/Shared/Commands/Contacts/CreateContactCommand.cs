using ContactList.Application.Shared.DTOs;
using MediatR;


namespace ContactList.Application.Shared.Commands.Contacts
{
    public class CreateContactCommand : IRequest<ContactResponseDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
    }
}
