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
        [Route("DeleteRun")]
        [HttpPost]
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
