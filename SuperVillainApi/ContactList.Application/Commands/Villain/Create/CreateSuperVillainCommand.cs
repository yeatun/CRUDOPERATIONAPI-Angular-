using ContactList.Application.DTOs;
using ContactList.Application.Mapper;
using ContactList.Core.Entities;
using ContactList.Core.Repositories.Command.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Commands.Villain.Create
{
    public class CreateSuperVillainCommand : IRequest<SuperVillainResponse>
    {
        /*public int Id { get; set; }*/
        public string? VillainName { get; set; }
        public string? Franchise { get; set; }
        public string? Powers { get; set; }
        public string? ImageURL { get; set; }
    }
    public class CreateSuperVillainCommandHandler : IRequestHandler<CreateSuperVillainCommand, SuperVillainResponse>
    {
        private readonly ISuperVillainCommandRepository _superVillainCommandRepository;
        public CreateSuperVillainCommandHandler(ISuperVillainCommandRepository superVillainCommandRepository)
        {
            _superVillainCommandRepository = superVillainCommandRepository;
        }
        public async Task<SuperVillainResponse> Handle(CreateSuperVillainCommand request, CancellationToken cancellationToken)
        {
            var superVillainEntity = SuperVillainMapper.Mapper.Map<SuperVillain>(request);

            if (superVillainEntity is null)
            {
                throw new ApplicationException("There is a problem in mapper");
            }

            var newCustomer = await _superVillainCommandRepository.AddAsync(superVillainEntity);
            var superVillainResponse = SuperVillainMapper.Mapper.Map<SuperVillainResponse>(newCustomer);
            return superVillainResponse;
        }

    }
}
