using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mahzan.Dapper
{
    public class DataConnection : IDisposable
    {
        private IDbConnection _connection;

        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State != ConnectionState.Open && _connection.State != ConnectionState.Connecting)
                {
                    _connection.Open();
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                }

                return _connection;
            }
        }

        public DataConnection(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
