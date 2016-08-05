using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using iBalekaAPI.Models.EventViewModels;
using Microsoft.AspNetCore.Identity;
using iBalekaAPI.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iBalekaAPI.Controllers
{
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
        [HttpGet(Name = "GetUserEvents")]
        public IActionResult GetUserEvents(string userId)
        {
            IEnumerable<Event> events = _context.GetEvents(userId);
            return Json(events);
        }
        [HttpGet(Name = "GetAthleteEvents")]
        public IActionResult GetAthleteEvents(string userId)
        {
            IEnumerable<Event> events = _context.GetEvents(userId);
            return Json(events);
        }
        // GET: Event/Details/5
        [HttpGet(Name = "GetEvent")]
        public IActionResult GetEvent(int id)
        {
            Event evnt = _context.GetEventByID(id);
            if (evnt == null)
            {
                return NotFound();
            }
            EventViewModel evntView = _context.GetEventByIDView(id);
            return Json(evntView);
        }
        //save event
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEvent(EventViewModel currentModel,string userId)
        {
            if (ModelState.IsValid)
            {
                currentModel.UserID = userId;
                _context.AddEvent(currentModel);

                return Ok();
                
            }
            else
            {
                return BadRequest();
            }
        }
        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(EventViewModel evnt)
        {

            if (ModelState.IsValid)
            {
                evnt.EventRoutes = new List<EventRouteViewModel>();

                foreach (int id in evnt.RouteId)
                {
                    evnt.EventRoutes.Add(new EventRouteViewModel(_routeContext.GetRouteByID(id)));
                }
                _context.UpdateEvent(evnt);
                _context.SaveEvent();

                return Json(evnt.EventId);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: Event/Delete/5
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