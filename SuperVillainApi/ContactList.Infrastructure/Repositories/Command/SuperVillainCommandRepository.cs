using ContactList.Core.Entities;
using ContactList.Core.Repositories.Command.Base;
using ContactList.Infrastructure.Data;
using ContactList.Infrastructure.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Repositories.Command
{
    public class SuperVillainCommandRepository : CommandRepository<SuperVillain>, ISuperVillainCommandRepository
    {
        public SuperVillainCommandRepository(SuperVillainDbContext context) : base(context)
        {

        }
    }
}
