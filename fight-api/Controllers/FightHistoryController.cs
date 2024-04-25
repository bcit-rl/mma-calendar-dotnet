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
    public class FightHistoryController : ControllerBase
    {
        private readonly CardContext _context;

        public FightHistoryController(CardContext context)
        {
            _context = context;
        }

        // GET: api/FightHistory
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<FightHistory>>> GetFightHistory()
        // {
        //     return await _context.FightHistories.ToListAsync();
        // }

        // GET: api/FightHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<FightHistory>>> GetFightHistory(int id)
        {
            var fightHistory = await _context.FightHistories.FromSql($"SELECT * FROM mma_calendar.FightHistory WHERE FighterId = {id} ORDER BY Date DESC LIMIT 5").ToListAsync();

            if (fightHistory == null)
            {
                return NotFound();
            }

            return fightHistory;
        }

        private bool FightHistoryExists(int id)
        {
            return _context.FightHistories.Any(e => e.FighterId == id);
        }
    }
}
