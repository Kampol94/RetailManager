using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        private readonly IConfiguration configuration;

        public ProductData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            
            var output = sql.LoadData<ProductModel, dynamic>("dbo.sqlProduct_GetAll", new { }, "RMData");

            return output;
        }

        public  ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);


            var output = sql.LoadData<ProductModel, dynamic>("dbo.sqlProduct_GetById", new { Id = productId }, "RMData").FirstOrDefault();

            return output;
        }
    }
}
