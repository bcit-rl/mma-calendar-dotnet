using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
    * This class will be used to build a Fighter object from the data
    * provided by the ESPN API.
    * 
    * The Fighter object will be used to populate the Fighter table in the
    * database.
    * 
*/
namespace ExtractService.Models
{
    public class FighterBuilder
    {
        private readonly HttpClient client = DBFiller.client;
        private readonly CardContext _context;
        public FighterBuilder(CardContext context)
        {
            _context = context;
        }   

        private void fillFighterStats(ref Fighter fighter, string responseBody){
                JObject? fighterRecord = JObject.Parse(responseBody);
                var items = fighterRecord["items"];
                if(items != null && items.First()["stats"] != null){
                    foreach (JToken item in items.First()["stats"]!){
                        switch((string?) item["name"])
                        {
                            case "wins":
                                fighter.Wins = (int?) item["value"];
                                break;
                            case "losses":
                                fighter.Losses = (int?) item["value"];
                                break;
                            case "draws":
                                fighter.Draws = (int?) item["value"];
                                break;
                            case "noContests":
                                fighter.NoContests = (int?) item["value"];
                                break;
                            default:
                                break;
                        }

                    }
                }
        }

        public async Task<Fighter> getFighter(string url)
        {
            Fighter athlete = new Fighter();
            try
            {
                var responseBody = await client.GetStringAsync(url);
                var fighterInfo = JObject.Parse(responseBody);

                if (fighterInfo["id"] != null)
                {
                    athlete.FighterId = (int) fighterInfo["id"]!;
                }
                
                athlete.FirstName = (string?)fighterInfo["firstName"];
                athlete.LastName = (string?)fighterInfo["lastName"];
                athlete.NickName = (string?)fighterInfo["nickname"];
                athlete.Weight = (int?)fighterInfo["weight"];
                athlete.Height = (int?)fighterInfo["height"];
                athlete.Age = (int?)fighterInfo["age"];
                athlete.Gender = (string?)fighterInfo["gender"];
                athlete.Citizenship = (string?)fighterInfo["citizenship"];
                if (fighterInfo["headshot"] is not null)
                {
                    athlete.Headshot = (string?) fighterInfo["headshot"]!["href"];
                }
                
                if (fighterInfo is not null  && fighterInfo["records"] is not null && fighterInfo["records"]!["$ref"] is not null )
                {
                    string recordResponse = await client.GetStringAsync((string) fighterInfo["records"]!["$ref"]!);
                    fillFighterStats(ref athlete, recordResponse);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return athlete;
        }

        public async Task<Fighter> checkFighter(string url)
        {
            Fighter new_fighter = await getFighter(url);
            //Check if fighter exits and updates or add based off of that information
            if (await _context.Fighters.FindAsync(new_fighter.FighterId) is Fighter fighter)
            {   
                _context.Entry(fighter).CurrentValues.SetValues(new_fighter);
            }
            else
            {
                _context.Fighters.Add(new_fighter);
            }
            await _context.SaveChangesAsync();

            return new_fighter;
        }

    }
}