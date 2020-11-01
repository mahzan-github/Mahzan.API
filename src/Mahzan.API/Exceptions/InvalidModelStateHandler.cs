using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mahzan.API.Exceptions
{
    public static class InvalidModelStateHandler
    {
        public static IActionResult Handler(ActionContext actionContext)
        {
            var result = new APIException
            {
                Message = FormatErrors(actionContext.ModelState),
            };

            return new BadRequestObjectResult(result);
        }

        private static string FormatSingle(ModelStateEntry modelStateEntry)
        {
            var errors = modelStateEntry.Errors.Select(error => error.ErrorMessage);
            return string.Join(", ", errors);
        }

        private static string FormatErrors(ModelStateDictionary keyValuePairs)
        {
            var pieces = keyValuePairs.Select(pair => $"{pair.Key}:{FormatSingle(pair.Value)}");

            return string.Join(", ", pieces);
        }
    }
}
