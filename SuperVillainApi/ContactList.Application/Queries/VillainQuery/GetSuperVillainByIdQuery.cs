using ContactList.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Queries.VillainQuery
{
    public class GetSuperVillainByIdQuery : IRequest<SuperVillain>
    {
        public Int64 Id { get; private set; }

        public GetSuperVillainByIdQuery(Int64 Id)
        {
            this.Id = Id;
        }
    }

    public class GetSuperVillainByIdHandler : IRequestHandler<GetSuperVillainByIdQuery, SuperVillain>
    {
        private readonly IMediator _mediator;

        public GetSuperVillainByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<SuperVillain> Handle(GetSuperVillainByIdQuery request, CancellationToken cancellationToken)
        {
            var villains = await _mediator.Send(new GetAllSuperVillainQuery());
            var selectedVillain = villains.FirstOrDefault(x => x.Id == request.Id);
            return selectedVillain;
        }
    }
}
