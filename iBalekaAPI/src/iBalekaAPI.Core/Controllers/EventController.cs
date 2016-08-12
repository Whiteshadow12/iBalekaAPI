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

        // GET: Event/Events
        [Route("GetUserEvents")]
        [HttpGet]
        public IActionResult GetUserEvents(string userId)
        {
            IEnumerable<Event> events = _context.GetUserEvents(userId);
            return Json(events);
        }
        [Route("GetEvents")]
        [HttpGet]
        public IActionResult GetEvents()
        {
            IEnumerable<Event> events = _context.GetEvents();
            return Json(events);
        }
        // GET: Event/Details/5
        [Route("GetEvent")]
        [HttpGet]
        public IActionResult GetEvent(int id)
        {
            Event evnt = _context.GetEventByID(id);
            if (evnt == null)
            {
                return NotFound();
            }
            return Json(evnt);
        }
        //save event
        [Route("SaveEvent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEvent(Event currentModel,string userId)
        {
            if (ModelState.IsValid)
            {
                currentModel.UserID = userId;
                _context.AddEvent(currentModel);
                _context.SaveEvent();
                return Ok();
                
            }
            else
            {
                return BadRequest();
            }
        }
        // POST: Event/Edit/5
        [Route("EditEvent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(Event evnt)
        {

            if (ModelState.IsValid)
            {
                evnt.EventRoute = new List<EventRoute>();

                foreach (int id in evnt.RouteId)
                {
                    evnt.EventRoute.Add(new EventRoute(_routeContext.GetRouteByID(id)));
                }
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
        [Route("DeleteEvent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(int id)
        {
            if (ModelState.IsValid)
            {
                Event deleteEvent = _context.GetEventByID(id);
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