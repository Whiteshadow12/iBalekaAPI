using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using Microsoft.AspNetCore.Identity;
using iBalekaAPI.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iBalekaAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EventController : Controller
    {
        private IEventService _context;
        private IRouteService _routeContext;
        
        public EventController(IEventService _repo, IRouteService _rContext)
        {
            _context = _repo;
            _routeContext = _rContext;
        }
        /// <summary>
        /// Get all user created events
        /// </summary>
        /// <param name="userId" type="int">User Id</param>
        /// <remarks>Get user created events</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        // GET: Event/Events
        [Route("GetUserEvents")]
        [HttpGet]
        public IActionResult GetUserEvents(string userId)
        {
            IEnumerable<Event> events = _context.GetUserEvents(userId);
            return Json(events);
        }
        /// <summary>
        /// Get all events
        /// </summary>
        /// <remarks>Get all events</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetEvents")]
        [HttpGet]
        public IActionResult GetEvents()
        {
            IEnumerable<Event> events = _context.GetEvents();
            return Json(events);
        }
        // GET: Event/Details/5
        /// <summary>
        /// Get a particular event
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Get an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetEvent")]
        [HttpGet]
        public IActionResult GetEvent(int eventId)
        {
            Event evnt = _context.GetEventByID(eventId);
            if (evnt == null)
            {
                return NotFound();
            }
            return Json(evnt);
        }
        //save event
        /// <summary>
        /// Saves an event
        /// </summary>
        /// <param name="evnt" type="Event">Event Model</param>
        /// <param name="userId" type="string">User Id</param>
        /// <remarks>Saves an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("SaveEvent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEvent(Event evnt,string userId)
        {
            if (ModelState.IsValid)
            {
                evnt.UserID = userId;
                _context.AddEvent(evnt);
                _context.SaveEvent();
                return Ok();
                
            }
            else
            {
                return BadRequest();
            }
        }
        // POST: Event/Edit/5
        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="evnt" type="Event">Event Model</param>
        /// <remarks>Update an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("EditEvent")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(Event evnt)
        {

            if (ModelState.IsValid)
            {

                //evnt.EventRoute = new List<EventRoute>();
                //IEnumerable<EventRoute> evntRoutes = _context.GetEventRoutes(evnt.EventId);
                //foreach (EventRoute route in evnt.EventRoute)
                //{
                //    evnt.EventRoute.Add(new EventRoute(_routeContext.GetRouteByID(route.RouteID)));
                //}
                _context.UpdateEvent(evnt);
                _context.SaveEvent();

                return Ok(evnt.EventId);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: Event/Delete/5
        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Delete an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeleteEvent")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(int eventId)
        {
            if (ModelState.IsValid)
            {
                Event deleteEvent = _context.GetEventByID(eventId);
                _context.Delete(deleteEvent);
                _context.SaveEvent();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}