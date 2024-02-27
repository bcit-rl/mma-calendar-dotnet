using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBClass.Models
{
    public class Fight
    {


        [Key]
        public int FightId { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }

        //Time when fight ended
        public string? DisplayClock{ get; set;}
        
        //Round the fight ended
        public int? Round { get; set; }

        //Method of victory
        public string? Method { get; set; }

        //Description of victory
        public string? MethodDescription { get; set; }

        //Card Segment ( Main Card, Prelims, Early Prelims)
        public string? CardSegment { get; set; }

        //A or B or Null
        public string? Winner { get; set;}

        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public int? FighterAId { get; set; }
        public Fighter? FighterA { get; set; }

        public int? FighterBId { get; set; }
        public Fighter? FighterB { get; set; }

        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }
}