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
    public class RunController : Controller
    {
        private IRunService _context;
        public RunController(IRunService _repo)
        {
            _context = _repo;
        }
        //GET: api/values
        /// <summary>
        /// Gets all athlete personal runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets all athlete personal runs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Athlete/Personal/[action]")]        
        public async Task<IActionResult> GetAthletePersonalRuns([FromQuery]int athleteId)
        {
            var response = new ListModelResponse<Run>()
               as IListModelResponse<Run>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Run> run = _context.GetAthletePersonalRuns(athleteId);
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
        /// <summary>
        /// Gets all athlete event runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets all athlete event runs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Athlete/Event/[action]")]
        public async Task<IActionResult> GetAthleteEventRuns([FromQuery]int athleteId)
        {
            var response = new ListModelResponse<Run>()
               as IListModelResponse<Run>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Run> run = _context.GetAthleteEventRuns(athleteId);
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
        /// <summary>
        /// Gets all runs by route
        /// </summary>
        /// <param name="routeId" type="int">Route Id</param>
        /// <remarks>Gets all runs by route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Route/[action]")]
        public async Task<IActionResult> GetRouteRuns([FromQuery]int routeId)
        {
            var response = new ListModelResponse<Run>()
               as IListModelResponse<Run>;
            try
            {
                if (routeId < 1)
                    throw new Exception("Route Id is missing");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Run> run = _context.GetRouteRuns(routeId);
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
        /// <summary>
        /// Gets all runs by event
        /// </summary>
        /// <param name="eventId" type="int">Event Id</param>
        /// <remarks>Gets all runs by event</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Event/[action]")]
        public async Task<IActionResult> GetEventRuns([FromQuery]int eventId)
        {
            var response = new ListModelResponse<Run>()
               as IListModelResponse<Run>;
            try
            {
                if (eventId < 1)
                    throw new Exception("Event Id is missing");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Run> run = _context.GetEventRuns(eventId);
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
        /// <summary>
        /// Gets all athletre runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets all athletr=e runs</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Athlete/[action]")]
        public async Task<IActionResult> GetAllRuns([FromQuery]int athleteId)
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
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetRun([FromQuery]int runId)
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

        /// <summary>
        ///  Gets total distance ran for a particular athlete
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets Athlete Total Distance</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Stats/Distance/[action]")]
        public async Task<IActionResult> GetTotalDistanceRan([FromQuery]int athleteId)
        {
            var response = new SingleModelResponse<double>()
               as ISingleModelResponse<double>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    double distance = -1;
                    distance = _context.GetTotalDistanceRan(athleteId);
                    if (distance < 0)
                        throw new Exception("Run does not exist");
                    return distance;
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
        ///  Gets a count of total runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets Total Run Count</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Stats/Run/[action]")]
        public async Task<IActionResult> GetRunCount([FromQuery]int athleteId)
        {
            var response = new SingleModelResponse<double>()
              as ISingleModelResponse<double>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    double run = -1;
                    run = _context.GetRunCount(athleteId);
                    if (run < 0)
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

        /// <summary>
        ///  Gets a count of event runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets Event Run Count</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Stats/Run/Event/[action]")]
        public async Task<IActionResult> GetEventRunCount([FromQuery]int athleteId)
        {
            var response = new SingleModelResponse<double>()
              as ISingleModelResponse<double>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    double eventCount = -1;
                    eventCount = _context.GetEventRunCount(athleteId);
                    if (eventCount < 0)
                        throw new Exception("Run does not exist");
                    return eventCount;
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
        ///  Gets a count of personal runs
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <remarks>Gets Personal Run Count</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Stats/Run/Personal/[action]")]
        public async Task<IActionResult> GetPersonalRunCount([FromQuery] int athleteId)
        {
            var response = new SingleModelResponse<double>()
              as ISingleModelResponse<double>;
            try
            {
                if (athleteId < 1)
                    throw new Exception("Athlete Id is missing");
                response.Model = await Task.Run(() =>
                {
                    double personalCount = -1;
                    personalCount = _context.GetPersonalRunCount(athleteId);
                    if (personalCount < 0)
                        throw new Exception("Run does not exist");
                    return personalCount;
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
        ///  Gets a calories burnt over a certain time
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <param name="startDate" type="int">Start Date</param>
        /// <param name="endDate" type="int">End Date</param>
        /// <remarks>Gets Calories burnt over time</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Stats/Calories/[action]")]
        public async Task<IActionResult> GetCaloriesOverTime([FromQuery] int athleteId, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            var response = new SingleModelResponse<double>()
              as ISingleModelResponse<double>;
            try
            {
                if ((athleteId.ToString()) == null
                    && (startDate.ToString()) == null
                    && (endDate.ToString()) == null)
                    throw new Exception("All parameters missing");
                else
                {
                    string exepMess = "";
                    if ((athleteId.ToString()) == null)
                        exepMess += "Athlete Id Missing\n";
                    if ((startDate.ToString()) == null)
                        exepMess += "Start Date missing\n";
                    if ((endDate.ToString()) == null)
                        exepMess += "End Date missing";
                    if (exepMess != "")
                        throw new Exception(exepMess);
                }


                response.Model = await Task.Run(() =>
                {
                    double calories = -1;
                    calories = _context.GetCaloriesOverTime(athleteId, startDate, endDate);
                    if (calories < 0)
                        throw new Exception("Run does not exist");
                    return calories;
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
        ///  Gets a total distance over a certain time
        /// </summary>
        /// <param name="athleteId" type="int">Athlete Id</param>
        /// <param name="startDate" type="int">Start Date</param>
        /// <param name="endDate" type="int">End Date</param>
        /// <remarks>Gets Total Distance burnt over time</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("Stats/Distance/Range/[action]")]
        public async Task<IActionResult> GetDistanceOverTime([FromQuery] int athleteId, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            var response = new SingleModelResponse<double>()
              as ISingleModelResponse<double>;
            try
            {
                if ((athleteId.ToString()) == null
                    && (startDate.ToString()) == null
                    && (endDate.ToString()) == null)
                    throw new Exception("All parameters missing");
                else
                {
                    string exepMess = "";
                    if ((athleteId.ToString()) == null)
                        exepMess += "Athlete Id Missing\n";
                    if ((startDate.ToString()) == null)
                        exepMess += "Start Date missing\n";
                    if ((endDate.ToString()) == null)
                        exepMess += "End Date missing";
                    if (exepMess != "")
                        throw new Exception(exepMess);
                }


                response.Model = await Task.Run(() =>
                {
                    double calories = -1;
                    calories = _context.GetDistanceOverTime(athleteId, startDate, endDate);
                    if (calories < 0)
                        throw new Exception("Runs do not exist");
                    return calories;
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
        [HttpPost]
        [Route("AddRun")]
        public async Task<IActionResult> AddRun([FromBody]Run run)
        {
            var response = new SingleModelResponse<Run>()
               as ISingleModelResponse<Run>;
            try
            {
                if (run == null)
                    throw new Exception("Run is missing");
                response.Model = await Task.Run(() =>
                {
                   Run runn= _context.AddRun(run);
                    return runn;
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
        [HttpPost]
        [Route("DeleteRun")]        
        public async Task<IActionResult> DeleteRun([FromQuery] int runId)
        {
            var response = new SingleModelResponse<Run>()
               as ISingleModelResponse<Run>;
            try
            {
                if (runId.ToString()!=null)
                    throw new Exception("Run Id is missing");
                response.Model = await Task.Run(() =>
                {
                    Run run = _context.GetRunByID(runId);
                    if (run == null)
                        throw new Exception("Run does not exist");
                    _context.Delete(runId);
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
