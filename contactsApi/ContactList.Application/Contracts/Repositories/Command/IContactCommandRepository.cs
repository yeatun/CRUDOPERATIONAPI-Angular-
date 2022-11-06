using ContactList.Application.Contracts.Repositories.Command.Base;
using ContactList.Core.Entities;

namespace ContactList.Application.Contracts.Repositories.Command
{
    public interface IContactCommandRepository : ICommandRepository<Contact>
    {
    }
}
