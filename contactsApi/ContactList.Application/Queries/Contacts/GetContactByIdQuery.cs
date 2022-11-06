using AutoMapper;
using ContactList.Application.Common.Exceptions;
using ContactList.Application.Contracts.Repositories.Query;
using ContactList.Application.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Queries.Contacts
{
    public class GetContactByIdQuery : IRequest<ContactResponseDTO>
    {
        public Guid Id { get; set; }
        public GetContactByIdQuery(Guid contactId)
        {
            Id = contactId;
        }
    }

    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactResponseDTO>
    {
        private readonly IContactQueryRepository _contactQueryRepository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IContactQueryRepository ContactQueryRepository, IMapper mapper)
        {
            _contactQueryRepository = ContactQueryRepository;
            _mapper = mapper;
        }
        public async Task<ContactResponseDTO> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contactEntity = await _contactQueryRepository.GetContactById(request.Id);
            if (contactEntity == null)
                throw new NotFoundException("contact not found");

            var contactDTO = _mapper.Map<ContactResponseDTO>(contactEntity);
            return contactDTO;
        }
    }
}
