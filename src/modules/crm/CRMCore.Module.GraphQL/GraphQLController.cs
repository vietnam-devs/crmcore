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

        /// <summary>
        /// Try as following
        /// { crm_Tasks_list(offset:1, first:10) { name } }
        /// { crm_Tasks_list { name } }
        /// { crm_Tasks(id: "5BEF390D-5B71-4DBA-853A-00E164D4EA93") { name } }
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Get([FromQuery] string query = "{ crm_Tasks_list(offset:1, first:10) { id, name } }")
        {
            var result = await new DocumentExecuter().ExecuteAsync(
                new ExecutionOptions()
                {
                    Schema = graphQLSchema,
                    Query = query                    
                }
            ).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return result.Errors.ToString();
            }

            var json = new DocumentWriter(indent: true).Write(result.Data);
            return json;
        }
    }
}
