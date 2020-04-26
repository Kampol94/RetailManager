using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleData _saleData;

        public SaleController(ISaleData saleData)
        {
            _saleData = saleData;
        }
        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _saleData.SaveSale(sale, userID);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> GetSaleReport()
        {
            return _saleData.GetSaleReport();
        }
    }
}