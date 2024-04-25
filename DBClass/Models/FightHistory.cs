using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBClass.Models
{
    [PrimaryKey("FighterId", "Date")]
    public class FightHistory
    {
        public int FighterId { get; set; }
        public DateTime Date { get; set; }

        public string? Opponent { get; set; }

        public string? Result { get; set; }

        public string? Method { get; set; }

        public string? Round { get; set; }

        public string? Time { get; set; }
        public string? Event { get; set; }

        public FightHistory(int fighterId, DateTime date, string? opponent, string? result, string? method , string? round, string? time, string? @event)
        {
            FighterId = fighterId;
            Date = date;
            Opponent = opponent;
            Result = result;
            Method = method;
            Round = round;
            Time = time;
            Event = @event;
        }

        public override string ToString()
        {
            return $"FighterId: {FighterId}\nDate: {Date}\nOpponent: {Opponent}\nResult: {Result}\nMethod: {Method}\nRound: {Round}\nTime: {Time}\nEvent: {Event}";
        }
    }
}