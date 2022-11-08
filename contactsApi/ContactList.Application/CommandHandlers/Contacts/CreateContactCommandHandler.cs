using AutoMapper;
using ContactList.Application.Contracts.Repositories;
using ContactList.Application.Contracts.Repositories.Command;
using ContactList.Application.Shared.Commands.Contacts;
using ContactList.Application.Shared.DTOs;
using ContactList.Core.Entities;
using MediatR;


namespace ContactList.Application.CommandHandlers.Contacts
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactResponseDTO>
    {
        private readonly IContactCommandRepository _contactCommandRepository;
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IContactCommandRepository contactCommandRepository, IUnitofWork unitOfWork, IMapper mapper)
        {
            _contactCommandRepository = contactCommandRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ContactResponseDTO> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contactEntity = _mapper.Map<SuperVillain>(request);
            var newContactEntity = await _contactCommandRepository.InsertAsync(contactEntity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<ContactResponseDTO>(newContactEntity);
        }
    }
}
