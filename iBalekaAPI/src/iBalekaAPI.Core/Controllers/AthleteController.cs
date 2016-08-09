using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("api/[controller]")]
    public class AthleteController : Controller
    {
        private IAthleteService _context;
        public AthleteController(IAthleteService _repo)
        {
            _context = _repo;
        }
        
        // GET: api/values
        [HttpGet]
        public IActionResult GetAthletes()
        {
            return Json(_context.GetAthletes());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetAthlete(int athleteId)
        {
            Athlete athlete = _context.GetAthlete(athleteId);
            if (athlete == null)
                return NotFound();
            return Json(athlete);
        }

        // POST api/values
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
        [HttpPut("{id}")]
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
        [HttpPut("{id}")] 
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
