using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardContextFactory;
using DBClass.Models;
using NuGet.Packaging;

namespace fight_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly CardContext _context;

        public VenueController(CardContext context)
        {
            _context = context;
        }

        // GET: api/Venue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenue()
        {
            List<Venue> venues = await _context.Venues.ToListAsync();
            foreach (var v in venues)
            {
                AddEventsToVenue(v);
            }
            return venues;
        }

        // GET: api/Venue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }
            AddEventsToVenue(venue);

            return venue;
        }



        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }

        private void AddEventsToVenue(Venue venue)
        {
            var stored_events = _context.Events.Where(e => e.VenueId == venue.VenueId).ToList();

            if (venue.Events == null)
                return;
            
            venue.Events.Clear();
            venue.Events.AddRange(stored_events);
        }

        // // PUT: api/Venue/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutVenue(int id, Venue venue)
        // {
        //     if (id != venue.VenueId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(venue).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!VenueExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Venue
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Venue>> PostVenue(Venue venue)
        // {
        //     _context.Venues.Add(venue);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetVenue", new { id = venue.VenueId }, venue);
        // }

        // // DELETE: api/Venue/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteVenue(int id)
        // {
        //     var venue = await _context.Venues.FindAsync(id);
        //     if (venue == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Venues.Remove(venue);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }
    }
}
