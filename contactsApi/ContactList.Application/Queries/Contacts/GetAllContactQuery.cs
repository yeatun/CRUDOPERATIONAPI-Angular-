using AutoMapper;
using ContactList.Application.Common.Constants;
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
    public class GetAllContactQuery : IRequest<PaginatedListResponseDTO<ContactResponseDTO>>
    {
        public int PageNumber { get; set; } = 1;

        //pageSize , pageNumber by default 1. 
        //if these values are unchanged that means pagination should not be applied
        private int _pageSize = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > PaginationConstants.MAX_PAGE_SIZE) ? PaginationConstants.MAX_PAGE_SIZE : value;
            }
        }
    
        public string? VillainName { get; set; }
        public string? Franchise { get; set; }
        public string? Powers { get; set; }
        public string? ImageURL { get; set; }
    }

    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, PaginatedListResponseDTO<ContactResponseDTO>>
    {
        private readonly IContactQueryRepository _contactQueryRepository;
        private readonly IMapper _mapper;
        public GetAllContactQueryHandler(IContactQueryRepository ContactQueryRepository, IMapper mapper)
        {
            _contactQueryRepository = ContactQueryRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedListResponseDTO<ContactResponseDTO>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            var (count, contactList) = await _contactQueryRepository.GetAllContactAsync(request);
            var contactResponseDTOList = _mapper.Map<IEnumerable<ContactResponseDTO>>(contactList);
            return new PaginatedListResponseDTO<ContactResponseDTO>(contactResponseDTOList, count, request.PageNumber, request.PageSize);

        }
    }
}
