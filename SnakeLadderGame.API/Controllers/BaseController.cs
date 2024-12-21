using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeLadderGame.Domain.Models;

namespace SnakeLadderGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Execute<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                if (result.IsValidationError || result.IsNormalError)
                {
                    return BadRequest(result);
                }
                else if (result.IsNotFoundError)
                {
                    return NotFound(result);
                }
                else if (result.IsServerError)
                {
                    return StatusCode(500, result);
                }

                return StatusCode(500, "Result Pattern Not Applied!");
            }
        }
    }
}
