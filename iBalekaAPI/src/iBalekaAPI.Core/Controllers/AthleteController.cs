using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using Swashbuckle.Swagger;

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
        public IActionResult GetAthletes()
        {
            return Json(_context.GetAthletes());
        }

        // GET api/values/5
        /// <summary>
        /// Returns a specific athlete 
        /// </summary>
        /// <param name="athleteId" type="int">AthleteId</param>
        /// <returns></returns>
        [Route("GetAthlete")]
        [HttpGet]
        public IActionResult GetAthlete(int athleteId)
        {
            Athlete athlete = _context.GetAthlete(athleteId);
            if (athlete == null)
                return NotFound();
            return Json(athlete);
        }

        // POST api/values
        /// <summary>
        /// Adds an Athlete to db
        /// </summary>
        /// <paramref name="athlete"/>
        [Route("AddAthlete")]
        [HttpPost]
        public IActionResult AddAthlete(Athlete athlete)
        {

            if (ModelState.IsValid)
            {
                _context.AddAthlete(athlete);
                _context.SaveAthlete();
                return Ok(athlete.AthleteId);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        /// <summary>
        /// Updates some properties of a athlete
        /// </summary>
        /// <param name="athlete" type="Athlete"></param>
        [Route("UpdateAthlete")]
        [HttpPut]
        public IActionResult UpdateAthlete(Athlete athlete)
        {

            if (ModelState.IsValid)
            {
                _context.UpdateAthlete(athlete);
                _context.SaveAthlete();
                return Ok(athlete.AthleteId);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/values/5
        /// <summary>
        /// Deletes a specific athlete
        /// </summary>
        /// <param name="athleteId"></param>
        [Route("DeleteAthlete")]
        [HttpPut]
        public IActionResult DeleteAthlete(int athleteId)
        {
            if (ModelState.IsValid)
            {
                Athlete athlete = _context.GetAthlete(athleteId);
                if (athlete == null)
                    return NotFound();
                _context.DeleteAthlete(athlete);
                _context.SaveAthlete();
                return Ok();
            }
            else
                return BadRequest(ModelState);

        }
    }
}