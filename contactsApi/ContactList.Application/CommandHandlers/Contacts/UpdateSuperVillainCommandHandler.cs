using AutoMapper;
using ContactList.Application.Contracts.Repositories.Command;
using ContactList.Application.Contracts.Repositories;
using ContactList.Application.Shared.Commands.Contacts;
using ContactList.Application.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Application.Contracts.Repositories.Query;
using ContactList.Core.Entities;
using ContactList.Application.Common.Exceptions;

namespace ContactList.Application.CommandHandlers.Contacts
{
    public class UpdateSuperVillainCommandHandler : IRequestHandler<UpdateContactCommand, ContactResponseDTO>
    {
        private readonly ISuperVillainCommandRepository _contactCommandRepository;
        private readonly ISuperVillainQueryRepository _contactQueryRepository;
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSuperVillainCommandHandler(ISuperVillainCommandRepository contactCommandRepository, ISuperVillainQueryRepository contactQueryRepository, IUnitofWork unitOfWork, IMapper mapper)
        {
            _contactCommandRepository = contactCommandRepository;
            _contactQueryRepository = contactQueryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ContactResponseDTO> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contactEntity = await _contactQueryRepository.GetContactById(request.Id);

            if(contactEntity == null)
                throw new NotFoundException("contact not found");

            var mappedContactEntity = _mapper.Map<UpdateContactCommand, SuperVillain>(request, contactEntity);

            var updatedContactEntity = await _contactCommandRepository.UpdateAsync(mappedContactEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<ContactResponseDTO>(updatedContactEntity);

        }
    }
}
