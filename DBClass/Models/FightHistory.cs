using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBClass.Models
{
    public class FightHistory
    {
        [Key]
        public int id { get; set; } 
        
        public required Fighter FighterId { get; set; }
         
        public DateTime? Date { get; set; }
        public string? Opponent { get; set; }


    }
}