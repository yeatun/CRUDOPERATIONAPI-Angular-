using ContactList.Application.Contracts.Repositories.Command;
using ContactList.Core.Entities;
using ContactList.Infrastructure.Persistance;
using ContactList.Infrastructure.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Repositories.Command
{
    public class ContactCommandRepository : CommandRepository<SuperVillain>, IContactCommandRepository
    {
        public ContactCommandRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
