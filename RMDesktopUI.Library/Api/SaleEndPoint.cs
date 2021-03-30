using RMDesktopUI.Library.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public class SaleEndPoint : ISaleEndPoint
    {
        private IAPIHelper _apiHelper;

        public SaleEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    //ToDo: Log successful 
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
