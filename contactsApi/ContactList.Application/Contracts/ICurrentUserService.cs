using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
    }
}
