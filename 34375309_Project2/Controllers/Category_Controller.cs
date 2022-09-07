using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _34375309_Project2.Models;
using Microsoft.AspNetCore.Authorization;

namespace _34375309_Project2.Controllers
{
    [Route("HS_34375309/[controller]")]
    [ApiController]
    //[Authorize]
    public class Category_Controller : ControllerBase
    {
        private readonly HSProjectdbdevContext _context;

        public Category_Controller(HSProjectdbdevContext context)
        {
            _context = context;
        }

        // GET: api/Cat_
        [HttpGet("Only retrieve CATEGORY info from database")]
        public async Task<ActionResult<IEnumerable<Models.Category>>> GetCategory()
        {
            return await _context.Category.ToListAsync();
        }

        // GET: api/Cat_/5
        [HttpGet("Only enter ID to retrieve CATEGORY info")]
        public async Task<ActionResult<Models.Category>> GetCategory(Guid id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // Tried adding security
        [HttpPut("Only enter ID for new CATEGORY entry")]
        public async Task<IActionResult> PutCategory(Guid id, Models.Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Cat_
        // Tried adding security
        [HttpPost("Only retrieve CATEGORY info from database")]
        public async Task<ActionResult<Models.Category>> PostCategory(Models.Category category)
        {
            _context.Category.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Cat_/5
        [HttpDelete("Only enter ID that you want to delete from CATEGORY")]
        public async Task<ActionResult<Models.Category>> DeleteCategory(Guid id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
