using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
    * This class will be used to build a Fight object from the data
    * provided by the ESPN API.
    * 
    * The Fight object will be used to populate the Fight table in the
    * database.
    * 
*/
namespace ExtractService.Models
{
    public class FightBuilder
    {
        private readonly HttpClient client = DBFiller.client; 
    }
}