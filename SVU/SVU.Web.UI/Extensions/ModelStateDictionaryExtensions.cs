using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public static string GetValidationErrors_LSV(this ModelStateDictionary modelState)
        {
            //Create the buidler for the errors
            var vaidiationErrors = new StringBuilder();

            //Go through the validation rules
            foreach (var error in modelState.Values)
            {
                //If the is a an error
                if (error.Errors.Any())
                {
                    //Append it to the strBuilder
                    vaidiationErrors.AppendLine($"{string.Join(',', error.Errors.Select(item => item.ErrorMessage))}.");
                }
            }
            return vaidiationErrors.ToString();
        }
    }
}
