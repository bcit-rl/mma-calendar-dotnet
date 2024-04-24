using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExtractService.Models
{
    public class FightHistoryBuilder
    {
        private readonly CardContext _context;

        public FightHistoryBuilder()
        {
            _context = CardContext.CreateDbContext([]);
        }

        public async Task<FightHistory> saveFightHistory(FightHistory new_history)
        {
            if (await _context.FightHistories.FindAsync([new_history.FighterId,new_history.Date]) is FightHistory history1)
            {
                _context.Entry(history1).CurrentValues.SetValues(new_history);
            }
            else
            {
                _context.FightHistories.Add(new_history);
            }
            

            await _context.SaveChangesAsync();
            _context.Entry(new_history).State = EntityState.Detached;

            return new_history;
        }
    }
}