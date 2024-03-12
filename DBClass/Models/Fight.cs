using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
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
        public string? DisplayClock { get; set; }

        //Round the fight ended
        public int? Round { get; set; }

        //Method of victory
        public string? Method { get; set; }

        //Description of victory
        public string? MethodDescription { get; set; }

        //Card Segment ( Main Card, Prelims, Early Prelims)
        public string? CardSegment { get; set; }

        //A or B or Null
        public string? Winner { get; set; }

        public int? EventId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        public int? MatchNumber {get; set;}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FighterAId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public int? FighterBId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Fighter? FighterA { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Fighter? FighterB { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Fighter>? Fighters { get; set;}
        public override string ToString()
        {
            //return "fds";
            return $"FightId: {FightId}\nDescription: {Description}\nDate: {Date}\nDisplayClock: {DisplayClock}\nRound: {Round}\nMethod: {Method}\nMethodDescription: {MethodDescription}\nCardSegment: {CardSegment}\nWinner: {Winner}\nFighterAId: {FighterAId}\nFighterBId: {FighterBId}\nEventId: {EventId}";
        }
    }
}