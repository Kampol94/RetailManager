using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}