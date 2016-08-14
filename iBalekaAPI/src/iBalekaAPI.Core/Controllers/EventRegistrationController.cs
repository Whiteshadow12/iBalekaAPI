using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EventRegistrationController : Controller
    {
        private IEventRegService _context;
        public EventRegistrationController(IEventRegService _repo)
        {
            _context = _repo;
        }
        // GET: api/values
        /// <summary>
        /// Get a particular event registration
        /// </summary>
        /// <param name="regId" type="int">Registration Id</param>
        /// <remarks>Get a particular event registration</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRegistration")]
        [HttpGet]
        public IActionResult GetRegistration(int regId)
        {
            if (ModelState.IsValid)
            {
                EventRegistration reg = _context.GetEventRegByID(regId);
                if (reg == null)
                    return NotFound();
                return Json(reg);
            }
            else
                return BadRequest(ModelState);
            
        }
        /// <summary>
        /// Get all registrations for a particular event 
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Get all registrations for a particular event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRegistrations")]
        [HttpGet]
        public IActionResult GetRegistrations(int eventId)
        {
            if (ModelState.IsValid)
            {
                return Json(_context.GetAll(eventId));
            }
            else
                return BadRequest();
        }
        /// <summary>
        /// Get all athlete registrations 
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Get all athlete registrations</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetAthleteRegistrations")]
        [HttpGet]
        public IActionResult GetAthleteRegistrations(int athleteId)
        {

            if (ModelState.IsValid)
            {
                return Json(_context.GetAthleteRegistrations(athleteId));
            }
            else
                return BadRequest(ModelState);
        }
        // POST api/values
        /// <summary>
        /// Register athlete
        /// </summary>
        /// <param name="reg" type="EventRegistration">Registration Model</param>
        /// <remarks>Register athlete</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("Register")]
        [HttpPost]
        public IActionResult Register(EventRegistration reg)
        {
            if (ModelState.IsValid)
            {
                _context.Register(reg);
                _context.SaveEventRegistration();
                return Ok(reg.RegistrationId);
            }
            else
                return BadRequest(ModelState);
        }

        // PUT api/values/5
        /// <summary>
        /// DeRegister athlete
        /// </summary>
        /// <param name="regId" type="int">Registration Id</param>
        /// <remarks>DeRegister athlete</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeRegister")]
        [HttpPut]
        public IActionResult DeRegister(int regId)
        {
            if (ModelState.IsValid)
            {
                _context.DeRegister(regId);
                _context.SaveEventRegistration();
                return Ok();
            }
            else
                return BadRequest(ModelState);
        }

        
    }
}
