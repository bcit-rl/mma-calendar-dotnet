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
        private readonly VenueBuilder _venueBuilder;
        private readonly CardContext _context;

        //private readonly string _url = "https://sports.core.api.espn.com/v2/sports/mma/leagues/ufc/calendar/ondays?lang=en&region=us";


        public DBFiller(CardContext context)
        {
            _context = context;
            _venueBuilder = new VenueBuilder(context);
            _eventBuilder = new EventBuilder(context);
            _fightBuilder = new FightBuilder(context);
            _fighterBuilder = new FighterBuilder(context);
        }

        public async Task run()
        {
            string url = "https://sports.core.api.espn.com/v2/sports/mma/leagues/ufc/calendar/ondays?lang=en&region=us";
            var responseBody = await client.GetStringAsync(url);
            var events_info = JObject.Parse(responseBody);
            List<string> event_urls = new List<string>();

            if (events_info["sections"] == null)
            {
                Console.WriteLine("No events found");
                return;
            }

            foreach (JToken event_info in events_info["sections"]!)
            {
                if (event_info["event"] != null && event_info["event"]!["$ref"] != null)
                {
                    event_urls.Add((string)event_info["event"]!["$ref"]!);
                }
            }

            foreach (string event_url in event_urls)
            {
                Console.WriteLine("adding event ");
                await addEvent(event_url);
            }


            Console.WriteLine("Done");
        }

        public async Task addEvent(string event_url)
        {
            Event? new_event = await _eventBuilder.getEvent(event_url);

            if (new_event == null)
            {
                throw new Exception("Event not found");
            }
            await _eventBuilder.saveEvent(new_event);

            List<string> fighturls = await _eventBuilder.getFightURLS(event_url);
            foreach (string fighturl in fighturls)
            {
                await _fightBuilder.createFight(fighturl, new_event);

            }
            await _eventBuilder.saveEvent(new_event);
        }

        /*
        *   Given a JToken and a list of keys, check if the key path exist in the JToken and 
        * that there are values throughout the entire key path. In this instance a key path
        * is the order of keys used to access a value in the response. 
        *
        * i.e
        *   response["event"]["$ref"] giving an event url. the keypath in this situation is
        *   ["event", "$ref"] in that order
        *
        *
        *   Return: true if the key path exists and has values throughout the entire path
        */
        public static bool checkKeyPathExists(JToken response, List<string> keys)
        {
            JToken? temp = response;
            foreach (string key in keys)
            {
                temp = temp[key];
                if (temp == null)
                {
                    return false;
                }
            }
            return true;
        }

    }
}