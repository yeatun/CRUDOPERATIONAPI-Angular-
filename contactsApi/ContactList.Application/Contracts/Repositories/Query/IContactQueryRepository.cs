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
    public interface IContactQueryRepository : IMultipleResultQueryRepository<SuperVillain>
    {
        public Task<(long, IEnumerable<SuperVillain>)> GetAllContactAsync(GetAllContactQuery queryParams);
        public Task<SuperVillain> GetContactById(int id);
    }
}
