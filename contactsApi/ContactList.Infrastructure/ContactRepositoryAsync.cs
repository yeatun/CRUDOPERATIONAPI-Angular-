using ContactList.Core.Entities;
using ContactList.Core.Interfaces;
using ContactList.Infrastructure.Data;
using ContactList.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure
{
    public class ContactRepositoryAsync : GenericRepositoryAsync<Contact>, IContactRepositoryAsync
    {
        private readonly DbSet<Contact> _contact;
        public ContactRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _contact = dbContext.Set<Contact>();
        }
    }
}
