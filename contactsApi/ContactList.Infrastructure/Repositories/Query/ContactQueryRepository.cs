using ContactList.Application.Contracts.Repositories.Query;
using ContactList.Application.Queries.Contacts;
using ContactList.Core.Entities;
using ContactList.Infrastructure.Configs;
using ContactList.Infrastructure.Repositories.Query.Base;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationSettings = ContactList.Infrastructure.Configs.ConfigurationSettings;

namespace ContactList.Infrastructure.Repositories.Query
{
    public class ContactQueryRepository : MultipleResultQueryRepository<Contact>, IContactQueryRepository
    {
        public ContactQueryRepository(IConfiguration configuration, IOptions<ConfigurationSettings> settings) : base(configuration, settings)
        {
        }

        public async Task<(long, IEnumerable<Contact>)> GetAllContactAsync(GetAllContactQuery queryParams)
        {
            int pageSize = queryParams.PageSize;
            int offset = (queryParams.PageNumber - 1) * pageSize;    //offset = (pageNumber -1) * pageSize
            string contactName = queryParams.FirstName;
            string contactNumber = queryParams.PhoneNumber;
            

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@offset", offset);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@FirstName", contactName);
            parameters.Add("@PhoneNumber", contactNumber);

            string query = "SELECT bu.Id , bu.FirstName , " +
                " bu.PhoneNumber , bu.Email , bu.LastName , bu.Company " +
                " FROM Contacts as bu";

            //include filter parameter 
            string filter = "";
            if (!string.IsNullOrWhiteSpace(contactName))
            {
                filter += " AND FirstName = @contactName ";
            }

            if (contactNumber!= null)
            {
                filter += " AND PhoneNumber = @PhoneNumber ";
            }

            //filter query condition is implemented if filter string length is greater than 0
            if (filter.Length > 0) query += filter;

            //pagination condition is implemented only if pageSize and offset value is valid
            if (pageSize > 1 && offset >= 0)
            {
                query += " ORDER BY (SELECT NULL) OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";
            }

            //query for getting count
            query += "; SELECT COUNT(*) AS count FROM Contacts " + filter + ";";


            return await base.GetMultipleResultAsync(query, parameters, false);
        }

        public async Task<Contact> GetContactById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            string query = "SELECT bu.Id , bu.FirstName , " +
                " bu.PhoneNumber , bu.Email , bu.LastName , bu.Company" +
                " FROM Contacts as bu WHERE Id = @Id";
            return await base.SingleAsync(query, parameters, false);

        }
    }
}
