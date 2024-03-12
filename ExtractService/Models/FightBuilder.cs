using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json.Nodes;
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
        private readonly CardContext _context;
        private readonly FighterBuilder _FighterBuilder;
        private readonly VenueBuilder _venueBuilder;

        public FightBuilder(CardContext context)
        {
            _context = CardContext.CreateDbContext([]);
            _FighterBuilder = new FighterBuilder(context);
            _venueBuilder = new VenueBuilder(context);
        }

        public async Task<Fight?> getFight(string url, Event fight_event)
        {
            Fight new_fight = new Fight();
            try
            {
                var responseBody = await client.GetStringAsync(url);
                var fightInfo = JObject.Parse(responseBody);
                JsonNode? fightStatus = null;

                if (fightInfo["id"] != null)
                {
                    new_fight.FightId = (int)fightInfo["id"]!;
                }
                new_fight.Description = (string?)fightInfo["description"];
                if (fightInfo["date"] != null)
                {
                    new_fight.Date = DateTime.Parse((string)fightInfo["date"], null, System.Globalization.DateTimeStyles.RoundtripKind);
                }

                if (fightInfo["cardSegment"] != null)
                {
                    new_fight.CardSegment = (string?)fightInfo["cardSegment"]!["description"];
                }

                if (fightInfo["matchNumber"] != null)
                {
                    new_fight.MatchNumber = (int?)fightInfo["matchNumber"];
                }
                new_fight.Winner = null;

                if (fightInfo["competitors"] != null)
                {
                    foreach (JToken competitor in fightInfo["competitors"]!)
                    {
                        Fighter fighter = await _FighterBuilder.checkFighter((string)competitor["athlete"]["$ref"]);
                        if ((int?)competitor["order"] == 1)
                        {
                            new_fight.FighterAId = fighter.FighterId;
                            //new_fight.FighterA = fighter;
                            if ((bool?)competitor["winner"] == true)
                            {
                                new_fight.Winner = "A";
                            }
                        }
                        else
                        {
                            new_fight.FighterBId = fighter.FighterId;
                            //new_fight.FighterB = fighter;
                            if ((bool?)competitor["winner"] == true)
                            {
                                new_fight.Winner = "B";
                            }
                        }

                    }
                }

                if (fightInfo["status"] != null)
                {
                    fightStatus = JsonObject.Parse(await client.GetStringAsync((string?)fightInfo["status"]!["$ref"]!));
                }

                if (fightStatus != null)
                {
                    new_fight.DisplayClock = (string?)fightStatus["displayClock"];
                    new_fight.Round = (int?)fightStatus["period"];
                    if (fightStatus["result"] != null)
                    {
                        new_fight.Method = (string?)fightStatus["result"]!["displayName"];
                        new_fight.MethodDescription = (string?)fightStatus["result"]!["displayDescription"];
                    }
                }

                // new_fight.VenueId = fight_event.EventLocationId;
                //new_fight.Venue = fight_event.EventLocation;
                new_fight.EventId = fight_event.EventId;
                //new_fight.Event = fight_event;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

            return new_fight;
        }

        public async Task<Fight> createFight(string url, Event fight_event)
        {
            Fight? fight = await getFight(url, fight_event);

            if (fight == null)
            {
                throw new Exception("Fight not found");
            }

            if (await _context.Fights.FindAsync(fight.FightId) is Fight fight1)
            {
                _context.Entry(fight1).CurrentValues.SetValues(fight);
            }
            else
            {
                _context.Fights.Add(fight);
            }
            //fight_event.Fights.Add(fight);

            await _context.SaveChangesAsync();
            //_context.Entry(fight).State = EntityState.Detached;

            return fight;
        }



    }
}