using ContactList.Application.Contracts.Repositories.Query.Base;
using ContactList.Infrastructure.Configs;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data;
using ConfigurationSettings = ContactList.Infrastructure.Configs.ConfigurationSettings;

namespace ContactList.Infrastructure.Repositories.Query.Base
{
    public class MultipleResultQueryRepository<TEntity> : QueryRepository<TEntity>, IMultipleResultQueryRepository<TEntity> where TEntity : class
    {
        protected MultipleResultQueryRepository(IConfiguration configuration, IOptions<ConfigurationSettings> settings) : base(configuration, settings)
        {
        }

        //Implementation of IMultipleResultQuery interface
        public async Task<(long, IEnumerable<TEntity>)> GetMultipleResultAsync(string sql, DynamicParameters parameters, bool isProcedure = false)
        {
            using (var connection = CreateConnection())
            {
                var grid = await connection.QueryMultipleAsync(sql, parameters, commandType: isProcedure ? CommandType.StoredProcedure : CommandType.Text);
                var result = await grid.ReadAsync<TEntity>();
                var count = (await grid.ReadAsync<long>()).FirstOrDefault();
                return (count, result);

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<IEnumerable<TEntity1>> GetAsync<TFirst, TSecond, TEntity1>(string sql, Func<TFirst, TSecond, TEntity1> map, DynamicParameters parameters, string splitters, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TEntity1 : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity1>> GetAsync<TFirst, TSecond, TThird, TFourth, TFifth, TEntity1>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TEntity1> map, DynamicParameters parameters, string splitters, bool isProcedure = false)
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TFourth : class
            where TFifth : class
            where TEntity1 : class
        {
            throw new NotImplementedException();
        }

        public Task<long> SingleCountAsync(string sql, DynamicParameters parameters, bool isProcedure = false)
        {
            throw new NotImplementedException();
        }
    }
}
