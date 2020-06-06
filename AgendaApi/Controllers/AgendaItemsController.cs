using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaApi.Models;


namespace AgendaApi.Controllers
{
    [Route("api/AgendaItems")]
    [ApiController]
    public class AgendaItemsController : ControllerBase
    {
        private readonly AgendaContext _context;

        public AgendaItemsController(AgendaContext context)
        {
            _context = context;
        }

        // GET: api/AgendaItems
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<AgendaItem>>> GetAgendaItems()
        {
            return await _context.AgendaItems.ToListAsync();
        }

        // GET: api/AgendaItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaItem>> GetAgendaItem(long id)
        {
            var agendaItem = await _context.AgendaItems.FindAsync(id);

            if (agendaItem == null)
            {
                return NotFound();
            }

            return agendaItem;
        }

        // PUT: api/AgendaItems/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendaItem(long id, AgendaItem agendaItem)
        {
            if (id != agendaItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(agendaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaItemExists(id))
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

        // POST: api/AgendaItems

        [HttpPost]
        public async Task<ActionResult<AgendaItem>> PostAgendaItem(AgendaItem agendaItem)
        {
            _context.AgendaItems.Add(agendaItem);
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetAgendaItem), new { id = agendaItem.Id }, agendaItem);
        }

        // DELETE: api/AgendaItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AgendaItem>> DeleteAgendaItem(long id)
        {
            var agendaItem = await _context.AgendaItems.FindAsync(id);
            if (agendaItem == null)
            {
                return NotFound();
            }

            _context.AgendaItems.Remove(agendaItem);
            await _context.SaveChangesAsync();

            return agendaItem;
        }

        private bool AgendaItemExists(long id)
        {
            return _context.AgendaItems.Any(e => e.Id == id);
        }
    }
}
