using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVU.Web.UI.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ModelStateDictionary"/>
    /// </summary>

    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Get the list of validation errors as Line seperated values(LSV)
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetValidationErrors(this ModelStateDictionary modelState)
        {
            //Create the buidler for the errors
            var vaidiationErrors = new List<string>();

            //Go through the validation rules
            foreach (var error in modelState.Values)
            {
                //If the is a an error
                if (error.Errors.Any())
                {
                    vaidiationErrors.Add($"{string.Join(',', error.Errors.Select(item => item.ErrorMessage))}.");
                }
            }
            return vaidiationErrors;
        }
    }
}
