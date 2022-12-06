using ContactList.Core.Repositories.Command.Base;
using ContactList.Core.Repositories.Command.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Commands.Villain.Delete
{
    public class DeleteSuperVillainCommand : IRequest<String>
    {
        public Int64 Id { get; private set; }
        public DeleteSuperVillainCommand(Int64 Id)
        {
            this.Id = Id;
        }
    }
    public class DeleteSuperVillainCommandHandler : IRequestHandler<DeleteSuperVillainCommand, String>
    {
        private readonly ISuperVillainCommandRepository _superVillainCommandRepository;
        private readonly ISuperVillainQueryRepository _superVillainQueryRepository;
        public DeleteSuperVillainCommandHandler(ISuperVillainCommandRepository superVillainCommandRepository, ISuperVillainQueryRepository superVillainQueryRepository)
        {
            _superVillainCommandRepository = superVillainCommandRepository;
            _superVillainQueryRepository = superVillainQueryRepository;
        }

        public async Task<string> Handle(DeleteSuperVillainCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerEntity = await _superVillainQueryRepository.GetByIdAsync(request.Id);

                await _superVillainCommandRepository.DeleteAsync(customerEntity);
            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "SuperVillain information has been deleted!";
        }
    }

}
