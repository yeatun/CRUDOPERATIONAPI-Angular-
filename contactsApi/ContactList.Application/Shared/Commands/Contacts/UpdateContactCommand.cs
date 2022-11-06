using ContactList.Application.Shared.DTOs;
using MediatR;


namespace ContactList.Application.Shared.Commands.Contacts
{
    public class UpdateContactCommand : IRequest<ContactResponseDTO>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
    }
}
