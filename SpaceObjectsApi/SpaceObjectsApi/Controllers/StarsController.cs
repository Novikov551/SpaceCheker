using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceObjectsApi.Models;
using SpaceObjectsApi.Repository.Interface;

namespace SpaceObjectsApi.Controllers
{
    [Route("api/[controller]")]
    public class StarsController : Controller
    {
        private readonly IRepository _repository;

        public StarsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Star>>> GetAsync()
        {
            var stars = await _repository.GetAsync<Star>();

            return Ok(stars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Star>> GetAsync(int id)
        {
            var star = await _repository.GetAsync<Star>(id);

            if (star == null)
            {
                return StatusCode(404);
            }

            return Ok(star);
        }

        [HttpPost, ActionName("Post")]
        public async Task<IActionResult> CreateAsync([FromBody] Star star)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(star);

                return Ok(star.Id);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpPut, ActionName("Put")]
        public async Task<ActionResult> UpdateAsync([FromBody] Star star)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateAsync(star);

                if (!result)
                {
                    return StatusCode(404);
                }

                return Ok(star);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var star = await _repository.GetAsync<Star>(id);

            var result = await _repository.RemoveAsync(star);

            if (!result)
            {
                return StatusCode(404);
            }

            return Ok();
        }
    }
}
