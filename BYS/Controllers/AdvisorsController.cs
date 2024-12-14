using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BYS.Data;
using BYS.Models;

namespace BYS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorsController : ControllerBase
    {
        private readonly RepositoryDbContext _context;

        public AdvisorsController(RepositoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Advisors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advisors>>> GetAdvisors()
        {
            return await _context.Advisors.ToListAsync();
        }

        // GET: api/Advisors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advisors>> GetAdvisors(int id)
        {
            var advisors = await _context.Advisors.FindAsync(id);

            if (advisors == null)
            {
                return NotFound();
            }

            return advisors;
        }

        // PUT: api/Advisors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvisors(int id, Advisors advisors)
        {
            if (id != advisors.AdvisorID)
            {
                return BadRequest();
            }

            _context.Entry(advisors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvisorsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Advisors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Advisors>> PostAdvisors(Advisors advisors)
        {
            _context.Advisors.Add(advisors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvisors", new { id = advisors.AdvisorID }, advisors);
        }

        // DELETE: api/Advisors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvisors(int id)
        {
            var advisors = await _context.Advisors.FindAsync(id);
            if (advisors == null)
            {
                return NotFound();
            }

            _context.Advisors.Remove(advisors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdvisorsExists(int id)
        {
            return _context.Advisors.Any(e => e.AdvisorID == id);
        }
    }
}
