using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _configs;
        public FactoryConnection(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CloseConnection()
        {
            if (_connection != null && _connection.State== ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (_connection== null)
            {
                _connection = new SqlConnection(_configs.Value.ConexionSql);
            }
            if (_connection.State!= ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }
    }
}
