using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ProductData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<ProductModel> GetProducts()
        {
            return _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.sqlProduct_GetAll", new { }, "RMData");
        }

        public ProductModel GetProductById(int productId)
        {
            return _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.sqlProduct_GetById", new { Id = productId }, "RMData").FirstOrDefault();
        }
    }
}
