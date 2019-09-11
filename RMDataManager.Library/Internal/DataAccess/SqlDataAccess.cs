using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library
{
    internal class SqlDataAccess : IDisposable
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters,string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public void SavedData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                
            }
        }

        private IDbConnection _connection;
        private IDbTransaction _transacion;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transacion = _connection.BeginTransaction();

        }

        public void SavedDataInTransaction<T>(string storedProcedure, T parameters)
        {
                  
                _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transacion);
            
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            
            
            
                List<T> rows = _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transacion).ToList();
                return rows;
            
        }

        public void ComitTransaction()
        {
            _transacion?.Commit();
            _connection?.Close();
        }

        public void RollBackTransaction()
        {
            _transacion?.Rollback();
            _connection?.Close();
        }

        public void Dispose()
        {

            ComitTransaction();
        }
    }
}
