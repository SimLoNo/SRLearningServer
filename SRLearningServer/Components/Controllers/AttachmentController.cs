using Microsoft.AspNetCore.Mvc;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _service;

        public AttachmentController(IAttachmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttachmentDto card)
        {
            try
            {
                AttachmentDto createdAttachment = await _service.Create(card);
                if (createdAttachment != null)
                {
                    return Ok(createdAttachment);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<AttachmentDto> attachments = await _service.GetAll();
                if (attachments.Count > 0)
                {
                    return Ok(attachments);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                AttachmentDto deactivatedAttachment = await _service.Deactivate(id);
                if (deactivatedAttachment != null)
                {
                    return Ok(deactivatedAttachment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                AttachmentDto deletedAttachment = await _service.Delete(id);
                if (deletedAttachment != null)
                {
                    return Ok(deletedAttachment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                AttachmentDto attachment = await _service.Get(id);
                if (attachment != null)
                {
                    return Ok(attachment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttachmentDto attachment)
        {
            try
            {
                AttachmentDto updatedAttachment = await _service.Update(attachment);
                if (updatedAttachment != null)
                {
                    return Ok(updatedAttachment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
