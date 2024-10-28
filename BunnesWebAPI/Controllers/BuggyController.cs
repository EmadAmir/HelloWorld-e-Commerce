using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebAPI.Controllers
{
    public class BuggyController : BaseApiController
    {

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotfound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("There is an internal server error");
        }

        [HttpGet("validationerror")]
        public IActionResult GetValidationError(Product product)
        {
            return Ok();
        }
    }
}
