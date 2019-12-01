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
    public static class DeleteTodo
    {
        [FunctionName("DeleteTodo")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "todo/{id}")]
            HttpRequestMessage req,
            string id,
            ILogger log)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) 
                    return req.CreateResponse(HttpStatusCode.OK);

                var documentLink = UriFactory.CreateDocumentUri(Constants.DatabaseId, Constants.CollectionId, id);

                using var client = DocumentDbClient.GetClient();

                await client.DeleteDocumentAsync(documentLink);

                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Failed to delete todo document. {ex.Message}");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}