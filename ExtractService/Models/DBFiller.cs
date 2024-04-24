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
using HtmlAgilityPack;


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
        private readonly FightHistoryBuilder _fightHistoryBuilder;
        private readonly CardContext _context;
        
        private readonly string _FIGHT_RECORD_URL = @"https://www.espn.com/mma/fighter/history/_/id/";

        //private readonly string _url = "https://sports.core.api.espn.com/v2/sports/mma/leagues/ufc/calendar/ondays?lang=en&region=us";


        public DBFiller(CardContext context)
        {
            _context = context;
            _venueBuilder = new VenueBuilder(context);
            _eventBuilder = new EventBuilder(context);
            _fightBuilder = new FightBuilder(context);
            _fighterBuilder = new FighterBuilder(context);
            _fightHistoryBuilder = new FightHistoryBuilder();
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

        public async Task fillFightRecords()
        {
            List<Fighter> fighters = _context.Fighters.ToList();
            foreach (Fighter fighter in fighters)
            {
                Console.WriteLine($"Getting fight record for {fighter.FirstName} {fighter.LastName}");
                await getFightRecord(fighter.FighterId);
            }
        }

        private async Task getFightRecord(int fighterId)
        {

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load($"{_FIGHT_RECORD_URL}{fighterId}");

            var node = htmlDoc.DocumentNode.SelectNodes("//tr");
            for(int index = 1 ; index < node.Count; index++)
            {   
                var childNodes = node[index].ChildNodes;

                string date = HtmlEntity.DeEntitize(childNodes[0].InnerText);
                string opponent = HtmlEntity.DeEntitize(childNodes[1].InnerText);
                string result = HtmlEntity.DeEntitize(childNodes[2].InnerText);
                string method = HtmlEntity.DeEntitize(childNodes[3].InnerText);
                string round = HtmlEntity.DeEntitize(childNodes[4].InnerText);
                string time =  HtmlEntity.DeEntitize(childNodes[5].InnerText);
                string eventName = HtmlEntity.DeEntitize(childNodes[6].InnerText);

                FightHistory fighterHistory = new FightHistory(fighterId, date, opponent, result, method, round, time, eventName);
                await _fightHistoryBuilder.saveFightHistory(fighterHistory);
            }

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