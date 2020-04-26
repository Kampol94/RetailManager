using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class InventoryData : IInventoryData
    {
        private readonly IConfiguration _configuration;
        private readonly ISqlDataAccess _sqlDataAccess;

        public InventoryData(IConfiguration configuration, ISqlDataAccess sqlDataAccess)
        {
            _configuration = configuration;
            _sqlDataAccess = sqlDataAccess;
        }
        public List<InventoryModel> GetInventory()
        {
            return _sqlDataAccess.LoadData<InventoryModel, dynamic>("dbo.sqlInventory_GetAll", new { }, "RMData");
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            _sqlDataAccess.SavedData("dbo.sqlInventory_Insert", item, "RMData");
        }
    }
}
