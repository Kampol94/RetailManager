using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library
{
    public class ConfigHelper 
    {
        public static decimal GetTaxtRate()
        {
            //string rateText = ConfigurationManager.AppSettings["taxRate"];
            string rateText = "8,75";
            bool IsValidTaxRate = Decimal.TryParse(rateText, out decimal output);

            if (!IsValidTaxRate)
            {
                throw new ConfigurationErrorsException();
            }

            return output;
        }
    }
}
