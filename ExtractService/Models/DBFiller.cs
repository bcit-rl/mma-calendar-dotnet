using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Common;
using Microsoft.CodeAnalysis.Elfie.Serialization;


namespace ExtractService.Models
{
    public class DBFiller
    {
        public static readonly HttpClient client = new HttpClient();

        //for testing purposes, should not be in final class
        private readonly FighterBuilder _fighterBuilder;
        private readonly EventBuilder _eventBuilder;
        private readonly FightBuilder _fightBuilder;
        private readonly VenueBuilder _venueBuilder ;
        private readonly CardContext _context;
        
        private readonly string _url = "https://sports.core.api.espn.com/v2/sports/mma/leagues/ufc/calendar/ondays?lang=en&region=us";


        public DBFiller(CardContext context)
        {
            _context = context;
            _venueBuilder = new VenueBuilder(context);
            _eventBuilder = new EventBuilder(context);
            _fightBuilder = new FightBuilder(context);
            _fighterBuilder = new FighterBuilder(context);
        }

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
            string url = "https://sports.core.api.espn.com/v2/sports/mma/leagues/ufc/calendar/ondays?lang=en&region=us";
            var responseBody = await client.GetStringAsync(url);
            var events_info = JObject.Parse(responseBody);
            List<string> event_urls = new List<string>();
            foreach (JToken event_info in events_info["sections"])
            {
                event_urls.Add((string)event_info["event"]["$ref"]);
            }

            foreach (string event_url in event_urls)
            {
                await addEvent(event_url);
            }

        }
        
        public async Task addEvent(string event_url){
            Event new_event = await _eventBuilder.createEvent(event_url);
            List<string> fighturls = await _eventBuilder.getFightURLS(event_url);
            foreach (string fighturl in fighturls)
            {
                await _fightBuilder.createFight(fighturl, new_event);
            }
        }

    }
}