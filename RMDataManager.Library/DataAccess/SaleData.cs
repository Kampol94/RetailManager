using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxtRate()/100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                var productInfo = products.GetProductById(item.ProductId);

                if(productInfo== null)
                {
                    throw new Exception($"{item.ProductId} brak w bazie");
                }

                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;

                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * taxRate;
                }


                details.Add(detail);
            }

            SAleDBModel sale = new SAleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            SqlDataAccess sql = new SqlDataAccess();

            sql.SavedData<SAleDBModel>("dbo.sqlSale_Insert", sale, "RMData");

             sale.Id = sql.LoadData<int, dynamic>("sqlSale_Lookup", new { sale.CashierId, sale.SaleDate }, "RMData").FirstOrDefault();

            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                sql.SavedData("dbo.sqlSaleDetail_Insert", item, "RMData");
            }

            

        }

        //public List<ProductModel> GetProducts()
        //{
        //    SqlDataAccess sql = new SqlDataAccess();


        //    var output = sql.LoadData<ProductModel, dynamic>("dbo.sqlProduct_GetAll", new { }, "RMData");

        //    return output;
        
    }
}
