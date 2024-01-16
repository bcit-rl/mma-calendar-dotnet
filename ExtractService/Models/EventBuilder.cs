using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
    * This class will be used to build an Event object from the data
    * provided by the ESPN API.
    * 
    * The Event object will be used to populate the Event table in the
    * database.
    * 
*/
namespace ExtractService.Models
{
    public class EventBuilder
    {
        private readonly HttpClient client = DBFiller.client; 

        
    }
}