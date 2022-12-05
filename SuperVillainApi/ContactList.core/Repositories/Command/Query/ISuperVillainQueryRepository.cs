using ContactList.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Core.Repositories.Command.Query
{
    public interface ISuperVillainQueryRepository : IQueryRepository<SuperVillain>
    {
        Task<IReadOnlyList<SuperVillain>> GetAllAsync();
        Task<SuperVillain> GetByIdAsync(int id);
    }
}
