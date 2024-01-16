using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExtractService.Models
{
    public class DBFiller
    {
        public static readonly HttpClient client = new HttpClient();
        private readonly FighterBuilder _fighterBuilder = new FighterBuilder();
        private readonly EventBuilder _eventBuilder = new EventBuilder();
        private readonly FightBuilder _fightBuilder = new FightBuilder();
        private readonly VenueBuilder _venueBuilder = new VenueBuilder();
        
        private readonly string _url = "https://sports.core.api.espn.com/v2/sports/mma/leagues/ufc/calendar/ondays?lang=en&region=us";

        //Johhny walker
        private readonly string _testFighterURL = "https://sports.core.api.espn.com/v2/sports/mma/athletes/3146944?lang=en&region=us";
        public static async Task GetDataFromEndpoint(string url)
        {
            try
            {
                // var responseBody = await client.GetStringAsync(url);
                // var json = JObject.Parse(responseBody);
                // Console.WriteLine(json);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }
        public async Task run()
        {
            //GetDataFromEndpoint(_url);
            Fighter johnny = await _fighterBuilder.getFighter(_testFighterURL);
            Console.WriteLine(johnny);
            
            Console.WriteLine("DBFiller Hello");
        }

    }
}