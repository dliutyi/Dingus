using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dingus.Server.Contexts;
using Dingus.Server.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dingus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardItemsController : ControllerBase
    {
        private readonly DingusContext _context;

        public DashboardItemsController(DingusContext context)
        {
            _context = context;
        }

        // GET: api/DashboardItems
        [HttpGet]
        public IEnumerable<DashboardItem> GetDashboardItems()
        {
            return _context.DashboardItems;
        }

        // GET: api/DashboardItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDashboardItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dashboardItem = await _context.DashboardItems.FindAsync(id);

            if (dashboardItem == null)
            {
                return NotFound();
            }

            return Ok(dashboardItem);
        }

        // PUT: api/DashboardItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDashboardItem([FromRoute] int id, [FromBody] DashboardItem dashboardItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dashboardItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(dashboardItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DashboardItemExists(id))
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

        // POST: api/DashboardItems
        [HttpPost]
        public async Task<IActionResult> PostDashboardItem([FromBody] DashboardItem dashboardItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DashboardItems.Add(dashboardItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDashboardItem", new { id = dashboardItem.Id }, dashboardItem);
        }

        // DELETE: api/DashboardItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDashboardItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dashboardItem = await _context.DashboardItems.FindAsync(id);
            if (dashboardItem == null)
            {
                return NotFound();
            }

            _context.DashboardItems.Remove(dashboardItem);
            await _context.SaveChangesAsync();

            return Ok(dashboardItem);
        }

        private bool DashboardItemExists(int id)
        {
            return _context.DashboardItems.Any(e => e.Id == id);
        }
    }
}