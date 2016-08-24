using System;
using System.Web.Http;

namespace Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage
{
    public class CalculatorController : ApiController
    {
        /// <summary>
        /// Sum two decimals
        /// </summary>
        /// <param name="decimal1"></param>
        /// <param name="decimal2"></param>
        /// <returns>Returns the sum of decimal1 and decimal2</returns>
        [HttpGet]
        public Decimal Sum(Decimal decimal1, Decimal decimal2)
        {
            var result = decimal1 + decimal2;
            Console.WriteLine($"Sum called: '{decimal1} + {decimal2} = {result}'");
            return result;
        }
    }
}
