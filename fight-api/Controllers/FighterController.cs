using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardContextFactory;
using DBClass.Models;

namespace fight_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FighterController : ControllerBase
    {
        private readonly CardContext _context;

        public FighterController(CardContext context)
        {
            _context = context;
        }

        // GET: api/Fighter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fighter>>> GetFighter()
        {
            return await _context.Fighters.ToListAsync();
        }

        // GET: api/Fighter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fighter>> GetFighter(int id)
        {
            var fighter = await _context.Fighters.FindAsync(id);

            if (fighter == null)
            {
                return NotFound();
            }

            return fighter;
        }

        private bool FighterExists(int id)
        {
            return _context.Fighters.Any(e => e.FighterId == id);
        }


        // // PUT: api/Fighter/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutFighter(int id, Fighter fighter)
        // {
        //     if (id != fighter.FighterId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(fighter).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!FighterExists(id))
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

        // // POST: api/Fighter
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Fighter>> PostFighter(Fighter fighter)
        // {
        //     _context.Fighters.Add(fighter);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetFighter", new { id = fighter.FighterId }, fighter);
        // }

        // // DELETE: api/Fighter/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteFighter(int id)
        // {
        //     var fighter = await _context.Fighters.FindAsync(id);
        //     if (fighter == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Fighters.Remove(fighter);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }
    }
}
