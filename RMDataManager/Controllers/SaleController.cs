using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using RMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {

        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userID = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userID);
        }

        
    }
}
