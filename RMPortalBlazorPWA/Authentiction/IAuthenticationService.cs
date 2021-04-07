using RMPortalBlazorPWA.Models;
using System.Threading.Tasks;

namespace RMPortalBlazorPWA.Authentiction
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationUserModel);
        Task LogOut();
    }
}