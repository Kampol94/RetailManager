﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        public SqlDataAccess(IConfiguration configuration, ILogger<SqlDataAccess> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }
        public string GetConnectionString(string name)
        {
            return configuration.GetConnectionString(name);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
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

            isClosed = false;

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

        private bool isClosed = false;
        private readonly IConfiguration configuration;
        private readonly ILogger<SqlDataAccess> _logger;

        public void ComitTransaction()
        {
            _transacion?.Commit();
            _connection?.Close();

            isClosed = true;
        }

        public void RollBackTransaction()
        {
            _transacion?.Rollback();
            _connection?.Close();

            isClosed = true;
        }

        public void Dispose()
        {
            if (!isClosed)
            {
                try
                {
                    ComitTransaction();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            _transacion = null;
            _connection = null;
        }
    }
}
