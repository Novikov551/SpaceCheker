using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceObjectsApi.Models;
using SpaceObjectsApi.Repository.Interface;

namespace SpaceChecker.Controllers
{
    [Route("api/[controller]")]
    public class PlanetsController : Controller
    {
        private readonly IRepository _repository;

        public PlanetsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Planet>>> GetAsync()
        {
            var planets = await _repository.GetAsync<Planet>();

            return Ok(planets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Planet>> GetAsync(int id)
        {
            var planet = await _repository.GetAsync<Planet>(id);

            if (planet == null)
            {
                return StatusCode(404);
            }

            return Ok(planet);
        }

        [HttpPost, ActionName("Post")]
        public async Task<IActionResult> CreateAsync([FromBody] Planet planet)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(planet);

                return Ok(planet.Id);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpPut, ActionName("Put")]
        public async Task<ActionResult> UpdateAsync([FromBody] Planet planet)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateAsync(planet);

                if (!result)
                {
                    return StatusCode(404);
                }

                return Ok(planet);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var planet = await _repository.GetAsync<Planet>(id);

            var result = await _repository.RemoveAsync(planet);

            if (!result)
            {
                return StatusCode(404);
            }

            return Ok();
        }
    }
}
