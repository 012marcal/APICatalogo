﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {

        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {

            _logger = logger;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {


            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("#############################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelStage : {context.ModelState.IsValid}");
            _logger.LogInformation("#############################################");


        }


        public void OnActionExecuted(ActionExecutedContext context)
        {

            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation("#############################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelStage : {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("#############################################");

        }


    }
}
