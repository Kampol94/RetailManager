using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData _productData;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly IConfiguration _configuration;

        public SaleData(IProductData productData, ISqlDataAccess sqlDataAccess, IConfiguration configuration)
        {
            _productData = productData;
            _sqlDataAccess = sqlDataAccess;
            _configuration = configuration;
        }

        public decimal GetTaxRate()
        {
            string rateText = _configuration.GetValue<string>("TaxRate");
            bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if (!IsValidTaxRate)
            {
                throw new ConfigurationErrorsException();
            }

            return output;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = GetTaxRate();

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                var productInfo = _productData.GetProductById(item.ProductId);

                if (productInfo == null)
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

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;


            try
            {
                _sqlDataAccess.StartTransaction("RMData");

                _sqlDataAccess.SavedData<SaleDBModel>("dbo.sqlSale_Insert", sale, "RMData");

                sale.Id = _sqlDataAccess.LoadDataInTransaction<int, dynamic>("sqlSale_Lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                foreach (var item in details)
                {
                    item.SaleId = sale.Id;
                    _sqlDataAccess.SavedDataInTransaction("dbo.sqlSaleDetail_Insert", item);
                }

                _sqlDataAccess.ComitTransaction();

            }
            catch
            {

                _sqlDataAccess.RollBackTransaction();
                throw;
            }


        }

        public List<SaleReportModel> GetSaleReport()
        {
            return _sqlDataAccess.LoadData<SaleReportModel, dynamic>("dbo.sqlSale_SaleReport", new { }, "RMData");
        }


    }
}
