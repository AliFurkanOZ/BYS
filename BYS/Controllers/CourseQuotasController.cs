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
    public class CourseQuotasController : ControllerBase
    {
        private readonly RepositoryDbContext _context;

        public CourseQuotasController(RepositoryDbContext context)
        {
            _context = context;
        }

        // GET: api/CourseQuotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseQuota>>> GetCourseQuotas()
        {
            return await _context.CourseQuotas.ToListAsync();
        }

        // GET: api/CourseQuotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseQuota>> GetCourseQuota(int id)
        {
            var courseQuota = await _context.CourseQuotas.FindAsync(id);

            if (courseQuota == null)
            {
                return NotFound();
            }

            return courseQuota;
        }

        // PUT: api/CourseQuotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseQuota(int id, CourseQuota courseQuota)
        {
            if (id != courseQuota.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(courseQuota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseQuotaExists(id))
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

        // POST: api/CourseQuotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseQuota>> PostCourseQuota(CourseQuota courseQuota)
        {
            _context.CourseQuotas.Add(courseQuota);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseQuotaExists(courseQuota.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseQuota", new { id = courseQuota.CourseId }, courseQuota);
        }

        // DELETE: api/CourseQuotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseQuota(int id)
        {
            var courseQuota = await _context.CourseQuotas.FindAsync(id);
            if (courseQuota == null)
            {
                return NotFound();
            }

            _context.CourseQuotas.Remove(courseQuota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseQuotaExists(int id)
        {
            return _context.CourseQuotas.Any(e => e.CourseId == id);
        }
    }
}
