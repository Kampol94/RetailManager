using RMDataManager.Library.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.Internal.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string Id);
    }
}