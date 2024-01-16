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
        
        public async Task<Fighter> getFighter(string url){
            try
            {
                var responseBody = await client.GetStringAsync(url);
                var json = JObject.Parse(responseBody);
                Fighter athlete = new Fighter();
                athlete.FirstName = json["athlete"]["firstName"].ToString();
                athlete.LastName = json["athlete"]["lastName"].ToString();
                athlete.NickName = json["athlete"]["weightClass"].ToString();
                athlete.Weight = json["athlete"]["firstName"].ToString();
                athlete.Height = json["athlete"]["lastName"].ToString();
                athlete.Age = json["athlete"]["weightClass"].ToString();
                athlete.Gender = json["athlete"]["firstName"].ToString();
                athlete.Citizenship = json["athlete"]["lastName"].ToString();
                athlete.Headshot = json["athlete"]["weightClass"].ToString();
                athlete.Wins = json["athlete"]["firstName"].ToString();
                athlete.Losses = json["athlete"]["lastName"].ToString();
                athlete.Draws = json["athlete"]["weightClass"].ToString();
                athlete.NoContests = json["athlete"]["firstName"].ToString();
                athlete.LeftStance = json["athlete"]["lastName"].ToString();
                athlete.RightStance = json["athlete"]["weightClass"].ToString();


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return new Fighter();
        }
        
    }
}