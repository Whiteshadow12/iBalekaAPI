using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;

namespace iBalekaAPI.Core.Controllers
{
    [Route("[controller]")]
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
        [HttpGet]
        [Route("User/[action]")]

        public async Task<IActionResult> GetUserEvents([FromQuery]string userId)
        {
            var response = new ListModelResponse<Event>() 
                as IListModelResponse<Event>;
            try
            {
                if (userId == null)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Event> evnts = _context.GetUserEvents(userId); ;
                    if (evnts == null)
                        throw new Exception("No Events");
                    return evnts;
                });
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Get all events
        /// </summary>
        /// <remarks>Get all events</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEvents()
        {
            var response = new ListModelResponse<Event>()
                as IListModelResponse<Event>;
            try
            {
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Event> evnts =_context.GetEvents();
                    if(evnts==null)
                        throw new Exception("No Events");
                    return evnts;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        // GET: Event/Details/5
        /// <summary>
        /// Get a particular event
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Get an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetEvent([FromQuery]int eventId)
        {
            var response = new SingleModelResponse<Event>()
                as ISingleModelResponse<Event>;
            try
            {
                if (eventId <1)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    Event evnt= _context.GetEventByID(eventId);
                    if (evnt == null)
                        throw new Exception("Event does not Exist");
                    return evnt;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        //save event
        /// <summary>
        /// Saves an event
        /// </summary>
        /// <param name="evnt" type="Event">Event Model</param>
        /// <remarks>Saves an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveEvent([FromBody]Event evnt)
        {
            var response = new SingleModelResponse<Event>()
               as ISingleModelResponse<Event>;
            try
            {
                if(evnt==null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.AddEvent(evnt);
                    _context.SaveEvent();
                    return evnt;                   
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
            
        }
        // POST: Event/Edit/5
        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="evnt" type="Event">Event Model</param>
        /// <remarks>Update an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("Update/[action]")]
        public async Task<IActionResult> EditEvent([FromBody]Event evnt)
        {

            var response = new SingleModelResponse<Event>()
               as ISingleModelResponse<Event>;
            try
            {
                if (evnt == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.UpdateEvent(evnt);
                    _context.SaveEvent();
                    return evnt;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // POST: Event/Delete/5
        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="evnt" type="int">Event Id</param>
        /// <remarks>Delete an event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent([FromQuery]int evnt)
        {
            var response = new SingleModelResponse<Event>()
               as ISingleModelResponse<Event>;
            try
            {
                if (evnt.ToString() == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.Delete(evnt);
                    _context.SaveEvent();
                    Event dEvent = new Event();
                    dEvent.EventId = evnt;
                    return dEvent;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();

        }
    }
}