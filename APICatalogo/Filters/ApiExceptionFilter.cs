﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<ApiExceptionFilter> _logger;


        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {

            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Ocorreu um erro não tratado: Status code 500");

            context.Result = new ObjectResult("Ocorreu um erro em sua solicitação")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
