using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DBClass.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public bool? Indoor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Event>? Events { get; set;}
        
        public override string ToString()
        {
            if (Events != null)
            {
                for (int i = 0; i < Events.Count; i++)
                {
                    Console.WriteLine(Events.ElementAt(i));
                }
            }

            return $"VenueId: {VenueId}\nName: {Name}\nCity: {City}\nState: {State}\nCountry: {Country}\nIndoor: {Indoor}\n";
        }
    }
}