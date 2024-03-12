using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DBClass.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string? EventName { get; set; }
        public string? ShortName { get; set; }
        public DateTime? EventDate { get; set; }

        public int? VenueId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [NotMapped]
        public List<int>? FightIdList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Fight>? Fights { get; set; }

        public override string ToString()
        {
            Console.WriteLine("Fights:");
            // foreach (var fight in Fights)
            // {
            //     Console.WriteLine(fight);
            // }
            return $"EventId: {EventId}\nEventName: {EventName}\nShortName: {ShortName}\nEventDate: {EventDate}\nVenueId: {VenueId}\n";
        }
    }
}