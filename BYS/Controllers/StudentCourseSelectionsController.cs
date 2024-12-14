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
    public class StudentCourseSelectionsController : ControllerBase
    {
        private readonly RepositoryDbContext _context;

        public StudentCourseSelectionsController(RepositoryDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourseSelections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourseSelections>>> GetStudentCourseSelections()
        {
            return await _context.StudentCourseSelections.ToListAsync();
        }

        // GET: api/StudentCourseSelections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourseSelections>> GetStudentCourseSelections(int id)
        {
            var studentCourseSelections = await _context.StudentCourseSelections.FindAsync(id);

            if (studentCourseSelections == null)
            {
                return NotFound();
            }

            return studentCourseSelections;
        }

        // PUT: api/StudentCourseSelections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCourseSelections(int id, StudentCourseSelections studentCourseSelections)
        {
            if (id != studentCourseSelections.SelectionID)
            {
                return BadRequest();
            }

            _context.Entry(studentCourseSelections).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCourseSelectionsExists(id))
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

        // POST: api/StudentCourseSelections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentCourseSelections>> PostStudentCourseSelections(StudentCourseSelections studentCourseSelections)
        {
            _context.StudentCourseSelections.Add(studentCourseSelections);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentCourseSelections", new { id = studentCourseSelections.SelectionID }, studentCourseSelections);
        }

        // DELETE: api/StudentCourseSelections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCourseSelections(int id)
        {
            var studentCourseSelections = await _context.StudentCourseSelections.FindAsync(id);
            if (studentCourseSelections == null)
            {
                return NotFound();
            }

            _context.StudentCourseSelections.Remove(studentCourseSelections);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCourseSelectionsExists(int id)
        {
            return _context.StudentCourseSelections.Any(e => e.SelectionID == id);
        }
    }
}
