using Microsoft.AspNetCore.Mvc;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeCategoryListController : ControllerBase
    {
        private readonly ITypeCategoryListService _service;

        public TypeCategoryListController(ITypeCategoryListService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<TypeCategoryListDto>> Create(TypeCategoryListDto entity)
        {
            try
            {
                var result = await _service.Create(entity);
                if (result == null)
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
        public async Task<ActionResult<TypeCategoryListDto>> Deactivate(int id)
        {
            try
            {
                var result = await _service.Deactivate(id);
                if (result == null)
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TypeCategoryListDto>> Delete(int entity)
        {
            try
            {
                var result = await _service.Delete(entity);
                if (result == null)
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TypeCategoryListDto>> Get(int id)
        {
            try
            {
                var result = await _service.Get(id);
                if (result == null)
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
        public async Task<ActionResult<IEnumerable<TypeCategoryListDto>>> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                if (result == null)
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

        [HttpGet("{name:string}")]
        public async Task<ActionResult<TypeCategoryListDto>> GetByName(string name)
        {
            try
            {
                var result = await _service.GetByName(name);
                if (result == null)
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

        [HttpPut]
        public async Task<ActionResult<TypeCategoryListDto>> Update(TypeCategoryListDto entity)
        {
            try
            {
                var result = await _service.Update(entity);
                if (result == null)
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
    }
}
