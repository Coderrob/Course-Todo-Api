using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace CourseTodo.Api
{
    public static class ReviewTodo
    {
        [FunctionName("ReviewTodo")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo/review")]
            HttpRequestMessage req,
            TraceWriter log)
        {
            // oh noz - exceptions
            throw new NotSupportedException("This feature has been disabled.");
        }
    }
}