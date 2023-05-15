using common.Model;
using Microsoft.AspNetCore.Mvc;
using tokengenerator.Model;
using tokengenerator.Service;

namespace tokengenerator.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardService _service;

        public CardController(ILogger<CardController> logger, ICardService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// List all existing cards
        /// </summary>
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.List());
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }

        }

        /// <summary>
        /// Get a card by id
        /// </summary>
        [HttpGet("{cardId}")]
        public IActionResult GetById(int cardId)
        {
            try
            {
                return Ok(_service.Get(cardId));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }

        }

        /// <summary>
        /// Save a new card and returns its token
        /// </summary>
        [HttpPost("")]
        public IActionResult Post([FromBody] CardInput card)
        {
            try
            {
                return Ok(_service.Save(card));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage(e.Message));
            }

        }
    }
}