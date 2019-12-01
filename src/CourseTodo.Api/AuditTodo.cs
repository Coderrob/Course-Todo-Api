using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CourseTodo.Api
{
    public static class AuditTodo
    {
        [FunctionName("AuditTodo")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "audit")]
            HttpRequestMessage req,
            ILogger log)
        {
            // oh noz - exceptions
            throw new NotSupportedException("This feature has been disabled.");
        }
    }
}