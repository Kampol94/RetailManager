using Microsoft.AspNet.Identity;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace RMDataManager.Controllers
{
    [Authorize]
    
    public class UserController : ApiController
    {
        

        // GET: User
        public List<UserModel> GetById()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();

            data.GetUserById(userID);

            return data.GetUserById(userID);
        }

       
    }
}

