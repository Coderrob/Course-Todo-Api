using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CourseTodo.Api
{
    public static class GetTodos
    {
        [FunctionName("GetTodos")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo/{id?}")]
            HttpRequestMessage req,
            string id,
            ILogger log)
        {
            try
            {
                var query = string.IsNullOrEmpty(id)
                    ? "SELECT * FROM d"
                    : $"SELECT * FROM d WHERE d.id = '{id}'";

                var collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseId, Constants.CollectionId);

                using var client = DocumentDbClient.GetClient();

                var feedOptions = new FeedOptions { EnableCrossPartitionQuery = true };
                var results = client.CreateDocumentQuery<Todo>(collectionLink, query, feedOptions).ToList();

                return req.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Failed to get todo documents. {ex.Message}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}