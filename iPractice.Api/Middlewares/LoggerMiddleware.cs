using iPractice.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Threading.Tasks;

namespace iPractice.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeaderKey = "X-Correlation-ID";


        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<LoggerMiddleware> logger, IOptions<AppConfiguration> appConfigurationOptions)
        {
            var header = context.Request.Headers[CorrelationIdHeaderKey];

            if (header.Count > 0)
            {
                using (logger.BeginScope("{@CorrelationId}", header[0]))
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
