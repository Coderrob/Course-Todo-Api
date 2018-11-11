using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace CourseTodo.Api
{
  public static class CreateTodo
  {
    [FunctionName("CreateTodo")]
    public static async Task<HttpResponseMessage> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todo")]
      HttpRequestMessage req,
      TraceWriter log)
    {
      try
      {
        var todo = await req.Content.ReadAsAsync<Todo>();

        if (string.IsNullOrEmpty(todo?.Task))
        {
          return req.CreateResponse(HttpStatusCode.BadRequest, "Task value was not provided.");
        }

        todo.Id = Guid.NewGuid();
        var collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseId, Constants.CollectionId);
        using (var client = DocumentDbClient.GetClient())
        {
          await client.CreateDocumentAsync(collectionLink, todo, disableAutomaticIdGeneration: true);          
          return req.CreateResponse(HttpStatusCode.Created, todo);
        }
      }
      catch (Exception ex)
      {
        return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
      }
    }
  }
}
