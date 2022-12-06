using ContactList.Infrastructure.Configs;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Data
{
    public class DbConnector
    {
        private readonly ConfigurationSettings _settings;
        private readonly IConfiguration _configuration;

        protected DbConnector(IConfiguration configuration,IOptions<ConfigurationSettings> settings)
        {
            _settings = settings.Value;
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var _connectionString = _settings.ConnectionStrings.ConfigurationDbConnection;
            return new SqliteConnection(_connectionString);
        }
    }
}
