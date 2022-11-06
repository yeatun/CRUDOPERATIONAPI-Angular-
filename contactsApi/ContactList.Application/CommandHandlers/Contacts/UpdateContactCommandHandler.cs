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
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactResponseDTO>
    {
        private readonly IContactCommandRepository _contactCommandRepository;
        private readonly IContactQueryRepository _contactQueryRepository;
        private readonly IUnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateContactCommandHandler(IContactCommandRepository contactCommandRepository, IContactQueryRepository contactQueryRepository, IUnitofWork unitOfWork, IMapper mapper)
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

            var mappedContactEntity = _mapper.Map<UpdateContactCommand, Contact>(request, contactEntity);

            var updatedContactEntity = await _contactCommandRepository.UpdateAsync(mappedContactEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<ContactResponseDTO>(updatedContactEntity);

        }
    }
}
