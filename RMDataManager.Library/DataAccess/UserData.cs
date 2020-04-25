using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Internal.DataAccess
{
    public class UserData       
    {
        private readonly IConfiguration configuration;

        public UserData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.sqlUserLookup", p, "RMData");

            return output;
        }
    }
}
