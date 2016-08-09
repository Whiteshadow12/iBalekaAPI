using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Controllers
{
    [Route("api/[controller]")]
    public class RunController : Controller
    {
        private IRunService _runRepo;
        public RunController(IRunService _repo)
        {
            _runRepo = _repo;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult GetAthletePersonalRuns(int athleteId)
        {
            IEnumerable<Run> runs = _runRepo.GetAthletePersonalRuns(athleteId);
            if (runs == null)
                return NoContent();
            return Json(runs);
        }
        [HttpGet("{id}")]
        public IActionResult GetAthleteEventRuns(int athleteId)
        {
            IEnumerable<Run> runs = _runRepo.GetAthleteEventRuns(athleteId);
            if (runs == null)
                return NoContent();
            return Json(runs);
        }
        [HttpGet("{id}")]
        public IActionResult GetRouteRuns(int routeId)
        {
            IEnumerable<Run> runs = _runRepo.GetRouteRuns(routeId);
            if (runs == null)
                return NoContent();
            return Json(runs);
        }
        [HttpGet("{id}")]
        public IActionResult GetEventRuns(int eventId)
        {
            IEnumerable<Run> runs = _runRepo.GetEventRuns(eventId);
            if (runs == null)
                return NoContent();
            return Json(runs);
        }
        [HttpGet("{id}")]
        public IActionResult GetAllRuns(int athleteId)
        {
            IEnumerable<Run> runs = _runRepo.GetAllRuns(athleteId);
            if (runs==null)
                return NoContent();
            return Json(runs);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetRun(int runId)
        {
            Run run = _runRepo.GetRunByID(runId);
            if (run == null)
                return NotFound();
            return Json(run);
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddRun(Run run)
        {
            if (ModelState.IsValid)
            {
                _runRepo.AddRun(run);
                _runRepo.SaveRun();
                return Ok(run.RunId);
            }
            else
                return BadRequest(ModelState);
        }
        // PUT api/values/5
        // DELETE api/values/5
        [HttpPost("{id}")]
        public IActionResult DeleteRun(int id)
        {
            Run run = _runRepo.GetRunByID(id);
            if (run == null)
                return NotFound();
            _runRepo.Delete(run);
            _runRepo.SaveRun();
            return Ok();
        }
    }
}
