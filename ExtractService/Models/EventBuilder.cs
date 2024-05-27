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
        private readonly CardContext _context;
        private readonly VenueBuilder _venueBuilder;

        public EventBuilder(CardContext context)
        {
            _context = CardContext.CreateDbContext([]);
            _venueBuilder = new VenueBuilder(context);
        }
        public async Task<Event?> getEvent(string url)
        {
            Event new_event = new Event();
            try
            {
                var responseBody = await client.GetStringAsync(url);
                var eventInfo = JObject.Parse(responseBody);
                if (eventInfo["id"] != null)
                {
                    new_event.EventId = (int)eventInfo["id"]!;
                }
                new_event.EventName = (string?)eventInfo["name"];
                new_event.ShortName = (string?)eventInfo["shortName"];
                if (eventInfo["date"] != null)
                {
                    new_event.EventDate = DateTime.Parse((string)eventInfo["date"]!, null, System.Globalization.DateTimeStyles.RoundtripKind);
                }
                if (eventInfo["venues"] != null && eventInfo["venues"].Count() > 0 && eventInfo["venues"]![0] != null && eventInfo["venues"]![0]!["$ref"] != null)
                {
                    Venue new_venue = await _venueBuilder.createVenue((string)eventInfo["venues"]![0]!["$ref"]!);
                    new_event.VenueId = new_venue.VenueId;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

            return new_event;
        }

        public async Task<Event> saveEvent(Event new_event)
        {
            if (await _context.Events.FindAsync(new_event.EventId) is Event event1)
            {
                _context.Entry(event1).CurrentValues.SetValues(new_event);
            }
            else
            {
                _context.Events.Add(new_event);
            }
            

            await _context.SaveChangesAsync();
            _context.Entry(new_event).State = EntityState.Detached;

            return new_event;
        }
        public async Task<Event> createEvent(string url)
        {
            Event? new_event = await getEvent(url);
            if (new_event == null)
            {
                throw new Exception("Event not found");
            }
            await saveEvent(new_event);

            return new_event;
        }

        /**
        * This method will be used to get the fight urls from the event url
        */
        public async Task<List<string>> getFightURLS(string url)
        {
            List<string> fightURLS = new List<string>();
            try
            {
                var responseBody = await client.GetStringAsync(url);
                var eventInfo = JObject.Parse(responseBody);

                if (eventInfo["competitions"] != null)
                {
                    foreach (JToken competition in eventInfo["competitions"]!)
                    {
                        if (competition["$ref"] != null)
                        {
                            fightURLS.Add((string)competition["$ref"]!);
                        }
                    }
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<string>();
            }

            return fightURLS;

        }

    }
}