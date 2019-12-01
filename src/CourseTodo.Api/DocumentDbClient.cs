using System;
using Microsoft.Azure.Documents.Client;

namespace CourseTodo.Api
{
    public class DocumentDbClient
    {
        public static DocumentClient GetClient()
        {
            var endpointUri = new Uri(Environment.GetEnvironmentVariable("EndpointUri"));
            var authKey = Environment.GetEnvironmentVariable("AuthKey");

            return new DocumentClient(endpointUri, authKey);
        }
    }
}