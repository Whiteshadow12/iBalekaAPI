using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using iBalekaAPI.Models.Responses;
using iBalekaAPI.Core.Extensions;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RunController : Controller
    {
        private IRunService _context;
        public RunController(IRunService _repo)
        {
            _context = _repo;
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
        public async Task<IActionResult> GetAllRuns(int athleteId)
        {
            var response = new ListModelResponse<Run>()
               as IListModelResponse<Run>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Run> run = _context.GetAllRuns(athleteId);
                    if (run == null)
                        throw new Exception("Run does not exist");
                    return run;
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
        ///  Gets a particular run
        /// </summary>
        /// <param name="runId" type="int">Run Id</param>
        /// <remarks>Gets a particular run</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRun")]
        [HttpGet]
        public async Task<IActionResult> GetRun(int runId)
        {
            var response = new SingleModelResponse<Run>()
               as ISingleModelResponse<Run>;
            try
            {
                if (runId < 1)
                    throw new Exception("Run Id is missing");
                response.Model = await Task.Run(() =>
                {
                    Run run = _context.GetRunByID(runId);
                    if (run == null)
                        throw new Exception("Run does not exist");
                    return run;
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
        /// Adds a run
        /// </summary>
        /// <param name="run" type="Run">Run Model</param>
        /// <remarks>Adds a run</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("AddRun")]
        [HttpPost]
        public async Task<IActionResult> AddRun(Run run)
        {
            var response = new SingleModelResponse<Run>()
               as ISingleModelResponse<Run>;
            try
            {
                if (run==null)
                    throw new Exception("Run is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.AddRun(run);
                    _context.SaveRun();
                    return run;
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
        /// Deletes a particular run
        /// </summary>
        /// <param name="runId" type="int">Run Id</param>
        /// <remarks>Deletes a particular run</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeleteRun")]
        [HttpPost]
        public async Task<IActionResult> DeleteRun(int runId)
        {
            var response = new SingleModelResponse<Run>()
               as ISingleModelResponse<Run>;
            try
            {
                if (runId < 1)
                    throw new Exception("Run Id is missing");
                response.Model = await Task.Run(() =>
                {
                    Run run = _context.GetRunByID(runId);
                    if (run == null)
                        throw new Exception("Run does not exist");
                    _context.Delete(run);
                    _context.SaveRun();
                    return run;
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
