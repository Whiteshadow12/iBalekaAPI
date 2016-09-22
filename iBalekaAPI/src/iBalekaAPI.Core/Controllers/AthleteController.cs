using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using Swashbuckle.Swagger;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public class AthleteController : Controller
    {
        private IAthleteService _context;
        public AthleteController(IAthleteService _repo)
        {
            _context = _repo;
        }

        // GET: api/values
        /// <summary>
        /// Gets all athletes
        /// </summary>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAthletes()
        {
            var response = new ListModelResponse<Athlete>()
                as IListModelResponse<Athlete>;
            try
            {
                response.Model = await Task.Run(() =>
                {
                    return _context.GetAthletes();
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // GET api/values/5
        /// <summary>
        /// Returns a specific athlete 
        /// </summary>
        /// <param name="athleteId" type="int">AthleteId</param>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAthlete([FromQuery] int athleteId)
        {
            var response = new SingleModelResponse<Athlete>()
                as ISingleModelResponse<Athlete>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is null");
                response.Model = await Task.Run(() =>
                {
                    Athlete athlete = _context.GetAthlete(athleteId);
                    if (athlete == null)
                        throw new Exception("Athlete does not Exist");
                    return athlete;
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
        /// Logs athlete in(Returns athlete Id) 
        /// </summary>
        /// <param name="username" type="int">Athlete Email</param>
        /// <param name="password" type="int">Athlete Password</param>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Account/[action]")]
        public async Task<IActionResult> LoginAthlete([FromQuery] string username, [FromQuery] string password)
        {
            var response = new SingleModelResponse<Athlete>()
                as ISingleModelResponse<Athlete>;
            try
            {
                if (username ==null)
                    throw new Exception("Athlete Email is null");
                if (password == null)
                    throw new Exception("Athlete Password is null");
                response.Model = await Task.Run(() =>
                {
                    Athlete athlete = _context.LoginAthlete(username,password);
                    if (athlete == null)
                        throw new Exception("Invalid Login Attempt");
                    return athlete;
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
        /// Return forgotton athlete
        /// </summary>
        /// <paramref name="email"/>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("Account/[action]")]
        public async Task<IActionResult> ForgotPassword([FromBody]string email)
        {

            var response = new SingleModelResponse<Athlete>()
                as ISingleModelResponse<Athlete>;
            try
            {
                if (email==null)
                    throw new Exception("Email Address is missing");
                response.Model = await Task.Run(() =>
                {
                    return _context.ForgotPassword(email);

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
        /// Registers an Athlete to db
        /// </summary>
        /// <paramref name="athlete"/>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterAthlete([FromBody]Athlete athlete)
        {

            var response = new SingleModelResponse<Athlete>()
                as ISingleModelResponse<Athlete>;
            var listResponse = new ListModelResponse<Athlete>()
                as IListModelResponse<Athlete>;
            try
            {
                if (athlete == null)
                    throw new Exception("Model is missing");
                listResponse.Model = await Task.Run(() =>
                {
                    return _context.GetAthletes();

                });
                foreach(Athlete existing in listResponse.Model)
                {
                    if(existing.EmailAddress == athlete.EmailAddress)
                        throw new Exception("Email address already in use.");
                    if (existing.UserName == athlete.UserName)
                        throw new Exception("Username is already in use.");
                }
                response.Model = await Task.Run(() =>
                {
                    return _context.RegisterAthlete(athlete);
                    
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
        /// Updates some properties of a athlete
        /// </summary>
        /// <param name="athlete" type="Athlete"></param>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAthlete([FromBody]Athlete athlete)
        {

            var response = new SingleModelResponse<Athlete>()
               as ISingleModelResponse<Athlete>;
            try
            {
                if (athlete == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    return _context.UpdateAthlete(athlete);
                    
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
   
        }

        // DELETE api/values/5
        /// <summary>
        /// Deletes a specific athlete
        /// </summary>
        /// <param name="athlete" type="int">Athlete Id</param>
        /// <remarks>Delete an Athlete</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAthlete([FromQuery]int athlete)
        {
            var response = new SingleModelResponse<Athlete>()
               as ISingleModelResponse<Athlete>;
            try
            {
                if (athlete.ToString() == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.DeleteAthlete(athlete);
                    _context.SaveAthlete();
                    Athlete at = new Athlete
                    {
                        AthleteId = athlete
                    };
                    return at;
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