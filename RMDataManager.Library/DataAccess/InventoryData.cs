using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class InventoryData
    {
        private readonly IConfiguration configuration;

        public InventoryData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            var output = sql.LoadData<InventoryModel, dynamic>("dbo.sqlInventory_GetAll", new { }, "RMData");

            return output;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            sql.SavedData("dbo.sqlInventory_Insert", item, "RMData");
        }
    }
}
