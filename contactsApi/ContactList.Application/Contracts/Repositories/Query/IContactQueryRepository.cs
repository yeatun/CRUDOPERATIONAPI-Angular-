using ContactList.Application.Contracts.Repositories.Query.Base;
using ContactList.Application.Queries.Contacts;
using ContactList.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts.Repositories.Query
{
    public interface IContactQueryRepository : IMultipleResultQueryRepository<Contact>
    {
        public Task<(long, IEnumerable<Contact>)> GetAllContactAsync(GetAllContactQuery queryParams);
        public Task<Contact> GetContactById(Guid id);
    }
}
