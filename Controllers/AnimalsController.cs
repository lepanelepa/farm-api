using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using farm_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace farm_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalDBContext _context;

        public AnimalsController(AnimalDBContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }
        

        // POST: api/Animals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal([FromBody] Animal animal)
        {
            if (!AnimalExists(animal.Name))
            {
                _context.Animals.Add(animal);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);             
            }
            else
            {
                return BadRequest();               
            }
         }

        // DELETE: api/Animals/5
        [HttpDelete("{name}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(string name)
        {
            var animal = await _context.Animals.FirstAsync(e => e.Name == name);       
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return animal;
        }

        private bool AnimalExists(string name)
        {
            return _context.Animals.Any(e => e.Name == name);
        }
    }
}
