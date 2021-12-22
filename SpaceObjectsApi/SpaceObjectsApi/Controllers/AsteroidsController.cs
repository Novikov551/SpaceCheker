using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceObjectsApi.Models;
using SpaceObjectsApi.Repository.Interface;

namespace SpaceChecker.Controllers
{
    [Route("api/[controller]")]
    public class AsteroidsController : Controller
    {
        private readonly IRepository _repository;

        public AsteroidsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Asteroid>>> GetAsync()
        {
            var asteroids = await _repository.GetAsync<Asteroid>();

            return Ok(asteroids);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asteroid>> GetAsync(int id)
        {
            var asteroid = await _repository.GetAsync<Asteroid>(id);

            if (asteroid == null)
            {
                return StatusCode(404);
            }

            return Ok(asteroid);
        }

        [HttpPost, ActionName("Post")]
        public async Task<IActionResult> CreateAsync([FromBody] Asteroid asteroid)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(asteroid);

                return Ok(asteroid.Id);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpPut, ActionName("Put")]
        public async Task<ActionResult> UpdateAsync([FromBody] Asteroid asteroid)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateAsync(asteroid);

                if (!result)
                {
                    return StatusCode(404);
                }

                return Ok(asteroid);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpDelete("{id}"), ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var asteroid = await _repository.GetAsync<Asteroid>(id);

            var result = await _repository.RemoveAsync(asteroid);

            if (!result)
            {
                return StatusCode(404);
            }

            return Ok();
        }
    }
}
