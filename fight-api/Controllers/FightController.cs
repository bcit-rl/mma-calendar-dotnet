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
    public class FightController : ControllerBase
    {
        private readonly CardContext _context;

        public FightController(CardContext context)
        {
            _context = context;
        }

        // GET: api/Fight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fight>>> GetFight()
        {
            return await _context.Fights.ToListAsync();
        }

        // GET: api/Fight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fight>> GetFight(int id)
        {
            var fight = await _context.Fights.FindAsync(id);
            
            if (fight == null)
            {
                return NotFound();
            }

            fillFighterData(fight, _context);
            
            return fight;
        }

        private bool FightExists(int id)
        {
            return _context.Fights.Any(e => e.FightId == id);
        }

        /**
        * Fills the fight data for an event. This is required as the ICollection in the model
        * is not being filled out by the entity framework. This due to how the models are set up
        * with circular references. This is a work around to get the data to be filled out.
        */
        public static void fillFighterData(Fight fight, CardContext _context)
        {
            fight.FighterA = _context.Fighters.Find(fight.FighterAId);
            fight.FighterB = _context.Fighters.Find(fight.FighterBId);
            fight.FighterAId = null;
            fight.FighterBId = null;

            if (fight.Winner != null){
                if (fight.Winner == "A" && fight.FighterA != null){
                    fight.FighterA.Winner = true;
                }else if (fight.Winner == "B" && fight.FighterB != null){
                    fight.FighterB.Winner = true;
                }
            }
        }
    }

            // // POST: api/Fight
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Fight>> PostFight(Fight fight)
        // {
        //     _context.Fights.Add(fight);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetFight", new { id = fight.FightId }, fight);
        // }

        // // DELETE: api/Fight/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteFight(int id)
        // {
        //     var fight = await _context.Fights.FindAsync(id);
        //     if (fight == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Fights.Remove(fight);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

                // // PUT: api/Fight/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutFight(int id, Fight fight)
        // {
        //     if (id != fight.FightId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(fight).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!FightExists(id))
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
}
