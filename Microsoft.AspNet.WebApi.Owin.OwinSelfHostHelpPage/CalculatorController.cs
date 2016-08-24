using System;
using System.Web.Http;

namespace Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage
{
    /// <summary>
    /// The Calculator controller
    /// </summary>
    public class CalculatorController : ApiController
    {
        /// <summary>
        /// Sum two decimals
        /// </summary>
        /// <param name="decimal1">Decimal 1</param>
        /// <param name="decimal2">Decimal 2</param>
        /// <returns>Returns the sum of decimal1 and decimal2</returns>
        [HttpGet]
        public Decimal Sum(Decimal decimal1, Decimal decimal2)
        {
            var result = decimal1 + decimal2;
            Console.WriteLine($"Sum called: '{decimal1} + {decimal2} = {result}'");
            return result;
        }

        /// <summary>
        /// Max two decimals
        /// </summary>
        /// <param name="decimal1">Decimal 1</param>
        /// <param name="decimal2">Decimal 2</param>
        /// <returns>Returns the max of decimal1 and decimal2</returns>
        [HttpGet]
        public Decimal Max(Decimal decimal1, Decimal decimal2)
        {
            var result = Math.Max(decimal1, decimal2);
            Console.WriteLine($"Sum called: 'Math.Max({decimal1}, {decimal2}) = {result}'");
            return result;
        }
    }
}
