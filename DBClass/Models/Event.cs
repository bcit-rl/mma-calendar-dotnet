using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBClass.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        
        public string? EventName { get; set;}
        public string? ShortName { get; set; }
        public DateTime? EventDate { get; set; }

        public int? EventLocationId { get; set; }
        public Venue? EventLocation { get; set; }

    }
}