using System.Collections.Generic;

namespace RMDataManager.Library
{
    public interface ISqlDataAccess
    {
        void ComitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollBackTransaction();
        void SavedData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SavedDataInTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}