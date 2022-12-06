using ContactList.Core.Entities;
using ContactList.Core.Repositories.Command.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Queries.VillainQuery
{
    public record GetAllSuperVillainQuery : IRequest<List<SuperVillain>>
    {

    }
    public class GetAllSuperVillainHandler : IRequestHandler<GetAllSuperVillainQuery, List<SuperVillain>>
    {
        private readonly ISuperVillainQueryRepository _superVillainQueryRepository;

        public GetAllSuperVillainHandler(ISuperVillainQueryRepository superVillainQueryRepository)
        {
            _superVillainQueryRepository = superVillainQueryRepository;
        }
        public async Task<List<SuperVillain>> Handle(GetAllSuperVillainQuery request, CancellationToken cancellationToken)
        {
            return (List<SuperVillain>)await _superVillainQueryRepository.GetAllAsync();
        }
    }
}
