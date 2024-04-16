using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardContextFactory;
using DBClass.Models;

namespace fight_api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly CardContext _context;

        public EventController(CardContext context)
        {
            _context = context;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            List<Event> events = await _context.Events.OrderBy(e => e.EventDate).ToListAsync();
            foreach (var e in events)
            {
                partiallyFillEventData(e);
            }

            return events;
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            fillEventData(@event);

            return @event;
        }

        //GET: api/Events/List
        //Gets a list of event IDs
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<int>>> GetEventList()
        {
            List<Event> events = await _context.Events.ToListAsync();
            List<int> event_ids = new List<int>();
            foreach (var e in events)
            {
                event_ids.Add(e.EventId);
            }

            return event_ids;
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }


        /*
        * Fills event data fully. By this I mean each fight and its information is filled out in
        * the json when returned/
        */
        private void fillEventData(Event unfilled_event)
        {
            fillVenueData(unfilled_event);
            fillFightData(unfilled_event);
        }

        /**
        * This function is to be used with getting all event data. It will give the event back
        * except instead of the list of fights it will give a list of fight ids. This prevents
        * too much information from being given to the user for a single event in a single request
        */
        private void partiallyFillEventData(Event unfilled_event)
        {
            fillVenueData(unfilled_event);
            fillFightIDs(unfilled_event);
        }

        /**
        * Fills the fight ids for an event. To be used when getting all event data.
        */
        private void fillFightIDs(Event event_no_fights)
        {
            var stored_fights = _context.Fights.Where(f => f.EventId == event_no_fights.EventId).OrderBy(f => f.MatchNumber).ToList();
            event_no_fights.FightIdList = new List<int>();
            foreach (Fight f in stored_fights)
            {
                event_no_fights.FightIdList.Add(f.FightId);
            }
            event_no_fights.Fights = null;
        }

        /**
        * Fills the venue data for an event. This is required as we need to set the event in the
        * venue to null to prevent a circular reference. This happens because venue's reference
        * events and events reference venues.
        */
        private void fillVenueData(Event event_no_venue)
        {
            event_no_venue.Venue = _context.Venues.Find(event_no_venue.VenueId);
            if (event_no_venue.Venue != null)
            {
                event_no_venue.Venue.Events = null;
            }
        }

        private void fillFightData(Event event_no_fights)
        {
            var stored_fights = _context.Fights.Where(f => f.EventId == event_no_fights.EventId).OrderBy(f => f.MatchNumber).ToList();

            foreach (Fight fight in stored_fights)
            {
                FightController.fillFighterData(fight, _context);
            }

            event_no_fights.Fights = stored_fights;
        }

    }
}
