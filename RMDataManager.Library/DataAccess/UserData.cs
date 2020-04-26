using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Internal.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public UserData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<UserModel> GetUserById(string Id)
        {
            return _sqlDataAccess.LoadData<UserModel, dynamic>("dbo.sqlUserLookup", new { Id }, "RMData");
        }
    }
}
