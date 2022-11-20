using AutoMapper;
using ContactList.Application.Common.Exceptions;
using ContactList.Application.Contracts.Repositories;
using ContactList.Application.Contracts.Repositories.Command;
using ContactList.Application.Contracts.Repositories.Query;
using ContactList.Application.Shared.Commands.Contacts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.CommandHandlers.Contacts
{
    public class DeleteSuperVillainCommandHandler : IRequestHandler<DeleteContactCommand, string>
    {
        private readonly ISuperVillainCommandRepository _contactCommandRepository;
        private readonly ISuperVillainQueryRepository _contactQueryRepository;
        private readonly IUnitofWork _unitOfWork;

        public DeleteSuperVillainCommandHandler(ISuperVillainCommandRepository contactCommandRepository, IUnitofWork unitOfWork, ISuperVillainQueryRepository contactQueryRepository)
        {
            _contactCommandRepository = contactCommandRepository;
            _contactQueryRepository = contactQueryRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<string> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contactEntity = await _contactQueryRepository.GetContactById(request.Id);

            if (contactEntity == null)
                throw new NotFoundException("contact not found");

            await _contactCommandRepository.DeleteAsync(contactEntity.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return "Contact Deleted";
        }
    }
}
