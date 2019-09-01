using System.Threading.Tasks;
using RMDesktopUI.Models;

namespace RMDesktopUI.Helper
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}