using ContactList.Application.Shared.DTOs;
using MediatR;


namespace ContactList.Application.Shared.Commands.Contacts
{
    public class CreateContactCommand : IRequest<ContactResponseDTO>
    {
       
        public string? VillainName { get; set; }
        public string? Franchise { get; set; }
        public string? Powers { get; set; }
        public string? ImageURL { get; set; }
    }
}
