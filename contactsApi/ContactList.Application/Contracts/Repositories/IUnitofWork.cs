using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts.Repositories
{
    public interface IUnitofWork
    {
       Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
