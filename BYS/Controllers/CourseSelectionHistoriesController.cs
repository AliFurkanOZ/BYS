﻿using System;
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
    public class CourseSelectionHistoriesController : ControllerBase
    {
        private readonly RepositoryDbContext _context;

        public CourseSelectionHistoriesController(RepositoryDbContext context)
        {
            _context = context;
        }

        // GET: api/CourseSelectionHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseSelectionHistory>>> GetCourseSelectionHistory()
        {
            return await _context.CourseSelectionHistory.ToListAsync();
        }

        // GET: api/CourseSelectionHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseSelectionHistory>> GetCourseSelectionHistory(int id)
        {
            var courseSelectionHistory = await _context.CourseSelectionHistory.FindAsync(id);

            if (courseSelectionHistory == null)
            {
                return NotFound();
            }

            return courseSelectionHistory;
        }

        // PUT: api/CourseSelectionHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseSelectionHistory(int id, CourseSelectionHistory courseSelectionHistory)
        {
            if (id != courseSelectionHistory.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(courseSelectionHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseSelectionHistoryExists(id))
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

        // POST: api/CourseSelectionHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseSelectionHistory>> PostCourseSelectionHistory(CourseSelectionHistory courseSelectionHistory)
        {
            _context.CourseSelectionHistory.Add(courseSelectionHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseSelectionHistoryExists(courseSelectionHistory.StudentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseSelectionHistory", new { id = courseSelectionHistory.StudentID }, courseSelectionHistory);
        }

        // DELETE: api/CourseSelectionHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseSelectionHistory(int id)
        {
            var courseSelectionHistory = await _context.CourseSelectionHistory.FindAsync(id);
            if (courseSelectionHistory == null)
            {
                return NotFound();
            }

            _context.CourseSelectionHistory.Remove(courseSelectionHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseSelectionHistoryExists(int id)
        {
            return _context.CourseSelectionHistory.Any(e => e.StudentID == id);
        }
    }
}