using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication_Assignment_SkillsLab2023.CustomeServerSideValidations
{
    public class CustomServerSideValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                var modelState = filterContext.Controller.ViewData.ModelState;
                var globalErrors = GetGlobalErrors(modelState);
                var fieldErrors = GetFieldErrors(modelState);

                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        GlobalErrors = globalErrors,
                        FieldErrors = fieldErrors
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                };
            }
        }

        private static List<string> GetGlobalErrors(ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }

        private static Dictionary<string, List<string>> GetFieldErrors(ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, List<string>>();

            foreach (var entry in modelState)
            {
                var key = entry.Key;
                var value = entry.Value;

                var errorMessages = value.Errors.Select(error => error.ErrorMessage).ToList();

                if (errorMessages.Any())
                {
                    errors.Add(key, errorMessages);
                }
            }

            return errors;
        }
    }
}
