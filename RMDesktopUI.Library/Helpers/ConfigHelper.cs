using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxtRate()
        {
            

            string rateText = ConfigurationManager.AppSettings["taxRate"];

             bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if(!IsValidTaxRate)
            {
                throw new ConfigurationErrorsException();
            }

            return output;
        }
    }
}
