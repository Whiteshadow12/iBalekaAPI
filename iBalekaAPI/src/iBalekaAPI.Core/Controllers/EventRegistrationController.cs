using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Services;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    //[Route("api/[controller]")]
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
        [Route("GetRegistration/{regId}")]
        [HttpGet]
        public async Task<IActionResult> GetRegistration(int regId)
        {
            var response = new SingleModelResponse<EventRegistration>()
                  as ISingleModelResponse<EventRegistration>;
            try
            {
                if (regId < 1)
                    throw new Exception("Registration Id is null");
                response.Model = await Task.Run(() =>
                {
                    EventRegistration regs = _context.GetEventRegByID(regId);
                    if (regs == null)
                        throw new Exception("No registrations");
                    return regs;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();

        }
        /// <summary>
        /// Get all registrations for a particular event 
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Get all registrations for a particular event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRegistrations/{eventId}")]
        [HttpGet]
        public async Task<IActionResult> GetRegistrations(int eventId)
        {
            var response = new ListModelResponse<EventRegistration>()
                 as IListModelResponse<EventRegistration>;
            try
            {
                if (eventId < 1)
                    throw new Exception("Athlete Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<EventRegistration> regs = _context.GetAll(eventId);
                    if (regs == null)
                        throw new Exception("No registrations");
                    return regs;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        /// <summary>
        /// Get all athlete registrations 
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Get all athlete registrations</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetAthleteRegistrations/{athleteId}")]
        [HttpGet]
        public async Task<IActionResult> GetAthleteRegistrations(int athleteId)
        {

            var response = new ListModelResponse<EventRegistration>()
                as IListModelResponse<EventRegistration>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<EventRegistration> regs= _context.GetAthleteRegistrations(athleteId);
                    if (regs==null)
                        throw new Exception("No registrations");
                    _context.SaveEventRegistration();
                    return regs;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }
        // POST api/values
        /// <summary>
        /// Register athlete
        /// </summary>
        /// <param name="reg" type="EventRegistration">Registration Model</param>
        /// <remarks>Register athlete</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("Register/{reg}")]
        [HttpPost]
        public async Task<IActionResult> Register(EventRegistration reg)
        {
            var response = new SingleModelResponse<EventRegistration>()
                as ISingleModelResponse<EventRegistration>;
            try
            {
                if (reg==null)
                    throw new Exception("Model is null");
                response.Model = await Task.Run(() =>
                {
                    _context.Register(reg);
                    _context.SaveEventRegistration();
                    return reg;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // PUT api/values/5
        /// <summary>
        /// DeRegister athlete
        /// </summary>
        /// <param name="regId" type="int">Registration Id</param>
        /// <remarks>DeRegister athlete</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeRegister/{regId}")]
        [HttpPut]
        public async Task<IActionResult> DeRegister(int regId)
        {
            var response = new SingleModelResponse<EventRegistration>()
                as ISingleModelResponse<EventRegistration>;
            try
            {
                if (regId < 1)
                    throw new Exception("Registration Id is null");
                response.Model = await Task.Run(() =>
                {
                    _context.DeRegister(regId);
                    _context.SaveEventRegistration();
                    return new EventRegistration();
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
