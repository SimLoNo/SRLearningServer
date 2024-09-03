using Microsoft.AspNetCore.Mvc;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _service;

        public ResultController(IResultService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResultDto entity)
        {
            try
            {
                var result = await _service.Create(entity);
                if (result is null)
                {
                    return BadRequest();
                }
                return Ok(result);
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
                var result = await _service.Deactivate(id);
                if (result is null)
                {
                    return BadRequest();
                }
                return Ok(result);
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
                ResultDto result = await _service.Delete(id);
                if (result is null)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.Get(id);
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
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

                var results = await _service.GetAll();
                if (results.Count > 0)
                {
                    return Ok(results);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ResultDto entity)
        {
            try
            {
                var result = await _service.Update(entity);
                if (result is null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
