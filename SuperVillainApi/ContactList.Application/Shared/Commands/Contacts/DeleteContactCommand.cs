using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Shared.Commands.Contacts
{
    public class DeleteContactCommand : IRequest<string>
    {
        public int Id { get; set; }
        public DeleteContactCommand(int contactId)
        {
            Id = contactId;
        }
    }
}
