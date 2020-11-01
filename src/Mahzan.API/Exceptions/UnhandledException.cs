using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mahzan.API.Exceptions
{
    public class UnhandledException : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger<UnhandledException> _logger;

        public UnhandledException(ILogger<UnhandledException> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var error = context.Exception;

            //Arguments Error
            if (error is ServiceArgumentException serviceArgumentException)
            {
                HandleServiceArgumentDataException(context, serviceArgumentException);
                return;
            }

            //Key Not Found
            if (error is ServiceKeyNotFoundException serviceKeyNotFoundException)
            {
                HandleServiceKeyNotFoundException(context, serviceKeyNotFoundException);
                return;
            }

            if (error is InvalidOperationException serviceInvalidOperationException)
            {
                HandleInvalidOperationException(context, serviceInvalidOperationException);
                return;
            }


            _logger.LogError(error, "Error ocurrido");

            var result = new APIException
            {
                Message = error.Message
            };

            context.Result = new BadRequestObjectResult(result);
        }

        private void HandleInvalidOperationException(
            ExceptionContext context,
            InvalidOperationException serviceInvalidOperationException)
        {
            APIException responseBase = new APIException
            {

                Message = serviceInvalidOperationException.Message
            };

            context.Result = new ObjectResult(responseBase)
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };
        }

        private void HandleServiceKeyNotFoundException(
            ExceptionContext context,
            ServiceKeyNotFoundException serviceKeyNotFoundException)
        {
            APIException responseBase = new APIException
            {
                Message = serviceKeyNotFoundException.Message
            };

            context.Result = new ObjectResult(responseBase)
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };
        }

        private void HandleServiceArgumentDataException(
            ExceptionContext context,
            ServiceArgumentException serviceArgumentException)
        {
            APIException responseBase = new APIException
            {
                Message = serviceArgumentException.Message
            };

            context.Result = new ObjectResult(responseBase)
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };
        }
    }
}
