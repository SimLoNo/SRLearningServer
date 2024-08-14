using Microsoft.AspNetCore.Mvc;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CardDto>> Create([FromBody] CardDto card)
        {
            try
            {
                if (card == null)
                {
                    return BadRequest("Card data is null.");
                }

                CardDto createdCard = await _service.Create(card);
                if (createdCard != null)
                {
                    return Ok(createdCard);
                }
                return BadRequest("Failed to create card.");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CardDto>> Deactivate(int id)
        {
            try
            {
                CardDto deactivatedCard = await _service.Deactivate(id);
                if (deactivatedCard != null)
                {
                    return Ok(deactivatedCard);
                }
                return BadRequest("Failed to deactivate card.");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CardDto>> Delete(int id)
        {
            try
            {
                CardDto deletedCard = await _service.Delete(id);
                if (deletedCard != null)
                {
                    return Ok(deletedCard);
                }
                return BadRequest("Failed to delete card.");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CardDto>> Get(int id)
        {
            try
            {
                CardDto card = await _service.Get(id);
                if (card != null)
                {
                    return Ok(card);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<CardDto> cards = await _service.GetAll();
                if (cards.Count > 0)
                {
                    return Ok(cards);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPost("GetByType")]
        public async Task<ActionResult<List<CardDto>>> GetByType([FromBody]List<List<TypeDto>> cards)
        {
            try
            {
                List<CardDto> card = await _service.GetByType(cards);
                if (card.Count > 0)
                {
                    return Ok(card);
                }
                return NoContent();

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CardDto>> Update([FromBody] CardDto card)
        {
            try
            {
                if (card == null)
                {
                    return BadRequest("Card data is null.");
                }

                CardDto updatedCard = await _service.Update(card);
                if (updatedCard != null)
                {
                    return Ok(updatedCard);
                }
                return BadRequest("Failed to update card.");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
