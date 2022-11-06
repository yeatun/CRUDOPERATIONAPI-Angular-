using ContactList.Infrastructure.Configs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationSettings = ContactList.Infrastructure.Configs.ConfigurationSettings;

namespace ContactList.Infrastructure.Persistance
{
    public class DbConnector
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigurationSettings _settings;
        public DbConnector(IConfiguration configuration, IOptions<ConfigurationSettings> settings)
        {
            _configuration = configuration;
            _settings = settings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = _settings.ConnectionStrings.ConfigurationDbConnection;
            return new SqlConnection(connectionString);
        }
    }
}
