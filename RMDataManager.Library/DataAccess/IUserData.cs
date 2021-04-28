using RMDataManager.Library.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.Internal.DataAccess
{
    public interface IUserData
    {
        void CreateUser(UserModel user);
        List<UserModel> GetUserById(string Id);
    }
}