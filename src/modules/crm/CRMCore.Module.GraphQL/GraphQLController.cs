using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRMCore.Module.GraphQL
{
    [Area("CRMCore.Module.GraphQL")]
    [Route("graphql/api/query")]
    public class GraphQLController : Controller
    {
        private readonly Schema graphQLSchema;

        public GraphQLController(Schema schema)
        {
            graphQLSchema = schema;
        }

        [HttpPost("")]
        public async Task<string> Get([FromQuery] string query = "{ crm_Tasks_list { id } }")
        {
            var result = await new DocumentExecuter().ExecuteAsync(
                new ExecutionOptions()
                {
                    Schema = graphQLSchema,
                    Query = query                    
                }
            ).ConfigureAwait(false);

            var json = new DocumentWriter(indent: true).Write(result.Data);
            return json;
        }
    }
}
