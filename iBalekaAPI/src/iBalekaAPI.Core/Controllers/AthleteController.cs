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
    [Route("api/[controller]")]
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
        [Route("GetAthletes")]
        [HttpGet]
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
        [Route("GetAthlete")]
        [HttpGet]
        public async Task<IActionResult> GetAthlete(int athleteId)
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
        [Route("AddAthlete")]
        [HttpPost]
        public async Task<IActionResult> AddAthlete(Athlete athlete)
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
        [Route("UpdateAthlete")]
        [HttpPut]
        public async Task<IActionResult> UpdateAthlete(Athlete athlete)
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
        [Route("DeleteAthlete")]
        [HttpPut]
        public async Task<IActionResult> DeleteAthlete(Athlete athlete)
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