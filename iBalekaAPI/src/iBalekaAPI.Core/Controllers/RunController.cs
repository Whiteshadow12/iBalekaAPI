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
    [Produces("application/json")]
    public class RunController : Controller
    {
        private IRunService _runRepo;
        public RunController(IRunService _repo)
        {
            _runRepo = _repo;
        }
        // GET: api/values
        //[Route("api/GetUserRoutes")]
        //[HttpGet]
        //public IActionResult GetAthletePersonalRuns(int athleteId)
        //{
        //    IEnumerable<Run> runs = _runRepo.GetAthletePersonalRuns(athleteId);
        //    if (runs == null)
        //        return NoContent();
        //    return Json(runs);
        //}
        //[Route("api/GetUserRoutes")]
        //[HttpGet]
        //public IActionResult GetAthleteEventRuns(int athleteId)
        //{
        //    IEnumerable<Run> runs = _runRepo.GetAthleteEventRuns(athleteId);
        //    if (runs == null)
        //        return NoContent();
        //    return Json(runs);
        //}
        //[Route("api/GetUserRoutes")]
        //[HttpGet]
        //public IActionResult GetRouteRuns(int routeId)
        //{
        //    IEnumerable<Run> runs = _runRepo.GetRouteRuns(routeId);
        //    if (runs == null)
        //        return NoContent();
        //    return Json(runs);
        //}
        //[Route("api/GetUserRoutes")]
        //[HttpGet]
        //public IActionResult GetEventRuns(int eventId)
        //{
        //    IEnumerable<Run> runs = _runRepo.GetEventRuns(eventId);
        //    if (runs == null)
        //        return NoContent();
        //    return Json(runs);
        //}
        /// <summary>
        /// Gets all athletr=e runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets all athletr=e runs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetAllRuns")]
        [HttpGet]
        public IActionResult GetAllRuns(int athleteId)
        {
            IEnumerable<Run> runs = _runRepo.GetAllRuns(athleteId);
            if (runs == null)
                return NoContent();
            return Json(runs);
        }
        // GET api/values/5
        /// <summary>
        ///  Gets a particular run
        /// </summary>
        /// <param name="runId" type="int">Run Id</param>
        /// <remarks>Gets a particular run</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRun")]
        [HttpGet]
        public IActionResult GetRun(int runId)
        {
            Run run = _runRepo.GetRunByID(runId);
            if (run == null)
                return NotFound();
            return Json(run);
        }
        // POST api/values
        /// <summary>
        /// Adds a run
        /// </summary>
        /// <param name="run" type="Run">Run Model</param>
        /// <remarks>Adds a run</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("AddRun")]
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
        // DELETE api/values/5
        /// <summary>
        /// Deletes a particular run
        /// </summary>
        /// <param name="runId" type="int">Run Id</param>
        /// <remarks>Deletes a particular run</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeleteRun")]
        [HttpPost]
        public IActionResult DeleteRun(int runId)
        {
            Run run = _runRepo.GetRunByID(runId);
            if (run == null)
                return NotFound();
            _runRepo.Delete(run);
            _runRepo.SaveRun();
            return Ok();
        }
    }
}
