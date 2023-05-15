using common.Model;
using Microsoft.AspNetCore.Mvc;
using tokengenerator.Model;
using tokengenerator.Service;

namespace tokengenerator.Controllers
{
    [ApiController]
    [Route("api/tokens")]
    public class TokensController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ITokenService _service;

        public TokensController(ILogger<CardController> logger, ITokenService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Validates the token against the data sent
        /// </summary>
        [HttpPost("validate")]
        public IActionResult Check([FromBody] ValidateInput input)
        {
            try
            {
                if (_service.ValidateToken(input))
                    return Ok();

                throw new Exception("Token does not match");
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }

        }

    }
}