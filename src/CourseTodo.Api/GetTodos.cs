using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace CourseTodo.Api
{
  public static class GetTodos
  {
    [FunctionName("GetTodos")]
    public static HttpResponseMessage Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo/{id?}")]
      HttpRequestMessage req,
      string id,
      TraceWriter log)
    {
      try
      {
        var query = string.IsNullOrEmpty(id)
          ? "SELECT * FROM d"
          : $"SELECT * FROM d WHERE d.id = '{id}'";
        var collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseId, Constants.CollectionId);
        using (var client = DocumentDbClient.GetClient())
        {
          var results = client.CreateDocumentQuery<Todo>(collectionLink, query).ToList();
          return req.CreateResponse(HttpStatusCode.OK, results);
        }
      }
      catch (Exception ex)
      {
        log.Error($"Failed to get todo documents. {ex.Message}", ex);
        return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
      }
    }
  }
}
