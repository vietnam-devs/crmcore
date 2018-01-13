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
        /// { tasks_list(offset:1, first:10) { name } }
        /// { tasks_list { name } }
        /// { tasks(id: "<task id>") { name } }
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
