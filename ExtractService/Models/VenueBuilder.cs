using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
/*
    * This class will be used to build a Venue object from the data
    * provided by the ESPN API.
    * 
    * The Venue object will be used to populate the Venue table in the
    * database.
    * 
*/
namespace ExtractService.Models
{
    public class VenueBuilder
    {
        private readonly HttpClient client = DBFiller.client;

        private readonly CardContext _context;

        public VenueBuilder(CardContext context)
        {
            _context = CardContext.CreateDbContext([]);
        }
        public async Task<Venue?> getVenue(string url)
        {
            Venue? venue = new Venue();
            try
            {
                var responseBody = await client.GetStringAsync(url);
                var venueInfo = JObject.Parse(responseBody);

                venue.VenueId = (int)venueInfo["id"]!;

                if (venueInfo["address"] != null)
                {
                    JToken address = venueInfo["address"]!;

                    venue.City = (string?)address["city"];
                    venue.State = (string?)address["state"];
                    venue.Country = (string?)address["country"];
                }

                venue.Name = (string?)venueInfo["fullName"];

                if (venueInfo["indoor"] != null)
                {
                    venue.Indoor = venueInfo["indoor"]!.ToObject<bool>();
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                venue = null;
            }

            return venue;
        }

        public async Task<Venue> addVenue(Venue venue)
        {

            if (await _context.Venues.FindAsync(venue.VenueId) is Venue venue1)
            {
                _context.Entry(venue1).CurrentValues.SetValues(venue);
            }
            else
            {
                _context.Venues.Add(venue);
            }
            //await _context.SaveChangesAsync();

            venue = _context.Entry(venue).Entity;

            return venue;
        }

        public async Task updateVenueEventList(int id, Event new_event)
        {
            Venue? venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                venue.Events.Add(new_event);
                if (await _context.Venues.FindAsync(id) is Venue venue1)
                {
                    _context.Entry(venue1).CurrentValues.SetValues(venue);
                    await _context.SaveChangesAsync();
                }
            }

        }

        public async Task<Venue> createVenue(string url)
        {
            Venue? venue = await getVenue(url);
            if (venue == null)
            {
                throw new ArgumentNullException(nameof(venue));
            }
            venue = await addVenue(venue);
            await _context.SaveChangesAsync();

            return venue;
        }

    }
}