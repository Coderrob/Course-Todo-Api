using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CourseTodo.Api
{
    public static class UpdateTodo
    {
        [FunctionName("UpdateTodo")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "todo/{id}")]
            HttpRequestMessage req,
            string id,
            ILogger log)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) 
                    return req.CreateResponse(HttpStatusCode.OK);

                var todo = await req.Content.ReadAsAsync<Todo>();

                todo.Id = Guid.Parse(id);

                var collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseId, Constants.CollectionId);

                using var client = DocumentDbClient.GetClient();

                await client.UpsertDocumentAsync(collectionLink, todo);

                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Failed to update todo document. {ex.Message}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}