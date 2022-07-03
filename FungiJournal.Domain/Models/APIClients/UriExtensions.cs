using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FungiJournal.Domain.Models.APIClients
{
    public static class UriExtensions
    {
        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        public static UriBuilder AddParameter(this UriBuilder uriBuilder, string paramName, string paramValue)
        {
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            String? decodedString = HttpUtility.UrlDecode(query.ToString());
            if (decodedString != null)
            {
                uriBuilder.Query = Uri.EscapeDataString(decodedString);
            }

            return uriBuilder;
        }
    }
}
