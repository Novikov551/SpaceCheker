using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceObjectsApi.Models;
using SpaceObjectsApi.Repository.Interface;

namespace SpaceObjectsApi.Controllers
{
    [Route("api/[controller]")]
    public class BlackHolesController : Controller
    {
        private readonly IRepository _repository;

        public BlackHolesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<BlackHole>>> GetAsync()
        {
            var blackHoles = await _repository.GetAsync<BlackHole>();

            return Ok(blackHoles);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlackHole>> GetAsync(int id)
        {
            var blackHole = await _repository.GetAsync<BlackHole>(id);

            if (blackHole == null)
            {
                return StatusCode(404);
            }

            return Ok(blackHole);
        }

        [HttpPost, ActionName("Post")]
        public async Task<IActionResult> CreateAsync([FromBody] BlackHole blackhole)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(blackhole);

                return Ok(blackhole.Id);
            }

            return BadRequest(ValidationProblem());
        }

        [HttpPut, ActionName("Put")]
        public async Task<ActionResult> UpdateAsync([FromBody] BlackHole blackHole)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.UpdateAsync(blackHole);

                if (!result)
                {
                    return StatusCode(404);
                }

                return Ok(blackHole);
            }

            return BadRequest(ValidationProblem());
        }


        [HttpDelete("{id}"), ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var blackHole = await _repository.GetAsync<BlackHole>(id);

            var result = await _repository.RemoveAsync(blackHole);

            if (!result)
            {
                return StatusCode(404);
            }

            return Ok();
        }
    }
}
