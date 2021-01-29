using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Solution.Api.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter( ILogger<CustomExceptionFilter> logger )
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var exception = context.Exception;

            _logger.LogInformation(exception, $"[x][{(int)status}]Erro: {exception.Message}");

            HttpResponse response = context.HttpContext.Response;

            //if (contextException.GetType() == typeof(NullReferenceException))
            //{
            //    status = HttpStatusCode.NotFound;
            //    result = new ValidationResult<string>(context.Exception, _resource.Instance(Null_Reference.CODE, Null_Reference.MESSAGE));
            //}

            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(new { Success = false, Error = "[500] Erro desconhecido!" });
        }
    }
}
