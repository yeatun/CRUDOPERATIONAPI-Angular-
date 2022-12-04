using ContactList.Core.Entities;
using ContactList.Core.Repositories.Command.Query;
using ContactList.Infrastructure.Repositories.Query.Base;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Repositories.Query
{
    public class SuperVillainQueryRepository : QueryRepository<SuperVillain>, ISuperVillainQueryRepository
    {
        public SuperVillainQueryRepository(IConfiguration configuration)
           : base(configuration)
        {

        }
        public async Task<IReadOnlyList<SuperVillain>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM SUPERVILLAIN";

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<SuperVillain>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
        public async Task<SuperVillain> GetByIdAsync(long id)
        {
            try
            {
                var query = "SELECT * FROM VILLAINS WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<SuperVillain>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
