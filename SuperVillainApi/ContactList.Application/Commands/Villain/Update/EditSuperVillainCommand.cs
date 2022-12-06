using ContactList.Application.DTOs;
using ContactList.Application.Mapper;
using ContactList.Core.Entities;
using ContactList.Core.Repositories.Command.Base;
using ContactList.Core.Repositories.Command.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Commands.Villain.Update
{
    public class EditSuperVillainCommand : IRequest<SuperVillainResponse>
    {
        public Int64 Id { get; set; }
        public string? VillainName { get; set; }
        public string? Franchise { get; set; }
        public string? Powers { get; set; }
        public string? ImageURL { get; set; }
    }

    public class EditSuperVillainCommandHandler : IRequestHandler<EditSuperVillainCommand, SuperVillainResponse>
    {
        private readonly ISuperVillainCommandRepository _superVillainCommandRepository;
        private readonly ISuperVillainQueryRepository _superVillainQueryRepository;
        public EditSuperVillainCommandHandler(ISuperVillainCommandRepository superVillainCommandRepository, ISuperVillainQueryRepository superVillainQueryRepository)
        {
            _superVillainCommandRepository = superVillainCommandRepository;
            _superVillainQueryRepository = superVillainQueryRepository;
        }
        public async Task<SuperVillainResponse> Handle(EditSuperVillainCommand request, CancellationToken cancellationToken)
        {
            var customerEntity = SuperVillainMapper.Mapper.Map<SuperVillain>(request);

            if (customerEntity is null)
            {
                throw new ApplicationException("There is a problem in mapper");
            }

            try
            {
                await _superVillainCommandRepository.UpdateAsync(customerEntity);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            var modifiedCustomer = await _superVillainQueryRepository.GetByIdAsync(request.Id);
            var customerResponse = SuperVillainMapper.Mapper.Map<SuperVillainResponse>(modifiedCustomer);

            return customerResponse;
        }
    }
}
