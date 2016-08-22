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
        /// 
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
        /// <returns></returns>
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



        // POST api/values
        /// <summary>
        /// Adds an Athlete to db
        /// </summary>
        /// <paramref name="athlete"/>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAthlete([FromBody]Athlete athlete)
        {

            var response = new SingleModelResponse<Athlete>()
                as ISingleModelResponse<Athlete>;
            try
            {
                if (athlete == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.AddAthlete(athlete);
                    _context.SaveAthlete();
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

        // PUT api/values/5
        /// <summary>
        /// Updates some properties of a athlete
        /// </summary>
        /// <param name="athlete" type="Athlete"></param>
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
                    _context.UpdateAthlete(athlete);
                    _context.SaveAthlete();
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

        // DELETE api/values/5
        /// <summary>
        /// Deletes a specific athlete
        /// </summary>
        /// <param name="athlete"></param>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAthlete([FromBody]Athlete athlete)
        {
            var response = new SingleModelResponse<Athlete>()
               as ISingleModelResponse<Athlete>;
            try
            {
                if (athlete == null)
                    throw new Exception("Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.DeleteAthlete(athlete);
                    _context.SaveAthlete();
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
    }
}