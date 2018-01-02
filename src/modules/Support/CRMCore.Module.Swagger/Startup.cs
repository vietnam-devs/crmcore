using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CRMCore.Module.Swagger
{
    public class FooController : Controller
    {
        [SwaggerOperation(Tags = new[] { "foo-group" })]
        [HttpGet("foo/test")]
        public IActionResult Get()
        {
            return Ok("foo");
        }
    }

    public class BarController : Controller
    {
        [SwaggerOperation(Tags = new[] { "foo-group" })]
        [HttpGet("foo/bar/test")]
        public IActionResult Get()
        {
            return Ok("bar");
        }
    }
}