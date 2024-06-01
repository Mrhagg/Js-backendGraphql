using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CourseProvider.Functions
{
    public class GraphQl(ILogger<GraphQl> logger, IGraphQLRequestExecutor graphQlRequestExecutor)
    {
        private readonly ILogger<GraphQl> _logger = logger;
        private readonly IGraphQLRequestExecutor _graphQlRequestExecutor = graphQlRequestExecutor;

        [Function("GraphQl")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "graphql")] HttpRequest req)
        {
          return await _graphQlRequestExecutor.ExecuteAsync(req); 
        }
    }
}
