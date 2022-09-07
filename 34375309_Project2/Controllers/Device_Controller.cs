using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _34375309_Project2.Models;
using _34375309_Project2.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace _34375309_Project2.Controllers
{
    [Route("HS_34375309/[controller]")]
    [ApiController]
    //[Authorize]
    public class Device_Controller : ControllerBase
    {
        private readonly HSProjectdbdevContext _context;

        public Device_Controller(HSProjectdbdevContext context)
        {
            _context = context;
        }

        // GET: api/Dev_
        [HttpGet("Only retrieve DEVICE info from database")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
        {
            return await _context.Device.ToListAsync();
        }

        // GET: api/Dev_/5
        [HttpGet("Only enter ID to retrieve info")]
        public async Task<ActionResult<Device>> GetDevice(Guid id)
        {
            var device = await _context.Device.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // PUT: api/Dev_/5
        //Tried security
        [HttpPut("Only enter ID for new DEVICE entry")]
        public async Task<IActionResult> PutDevice(Guid id, Device device)
        {
            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Dev_
        // Tried adding security
        [HttpPost("Only retrieve DEVICE info from database")]
        
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _context.Device.Add(device);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeviceExists(device.DeviceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        }

        // DELETE: api/Dev_/5
        [HttpDelete("Only enter ID that you want to delete from DEVICE")]
        public async Task<ActionResult<Device>> DeleteDevice(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Device.Remove(device);
            await _context.SaveChangesAsync();

            return device;
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }
    }
}
