using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

//using prototypeWeb.Models;

namespace iBalekaAPI.Core.Controllers
{
    [Produces("application/json")]
    public class MapController : Controller
    {
        private IRouteService _context;
        public MapController(IRouteService _repo)
        {
            _context = _repo;
        }



        //// GET: Map/SavedRoutes
        /// <summary>
        /// Get all user created Routes
        /// </summary>
        /// <param name="userId" type="string">User Id</param>
        /// <remarks>Get all user created Routes</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetUserRoutes")]
        [HttpGet]
        public IActionResult GetUserRoutes(string userId)
        {
            IEnumerable<Route> routes = _context.GetUserRoutes(userId);
            return Json(routes);
        }
        /// <summary>
        /// Get all Routes
        /// </summary>
        /// <remarks>Get all Routes</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRoutes")]
        [HttpGet]
        public IActionResult GetRoutes()
        {
            IEnumerable<Route> routes = _context.GetRoutes();
            return Json(routes);
        }

        //// POST: Map/AddRoute
        /// <summary>
        /// Add Route to DB
        /// </summary>
        /// <param name="route" type="Route">Route Model</param>
        /// <param name="userId" type="string">User Id</param>
        /// <remarks>Add Route to DB</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("AddRoute")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoute(Route route,string userId)
        {
            Route newRoute = route;
            if (ModelState.IsValid)
            {
                route.UserID = userId;
                _context.AddRoute(route);
                _context.SaveRoute();
                return Ok(route.RouteId);
             
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Debug.WriteLine("Errors found: "+ errors+"\nEnd Errors found");
                return BadRequest(ModelState);
            }
        }


        // GET: Map/Edit/5
        /// <summary>
        /// Get a Route
        /// </summary>
        /// <param name="routeId" type="int">Route Id</param>
        /// <remarks>Get a Route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetRoute")]
        [HttpGet]
        public IActionResult GetRoute(int routeId)
        {
            Route route = _context.GetRouteByID(routeId);
            if (route == null)
            {
                return NotFound();
            }

            return Json(route);
        }

        // POST: Map/Edit/5
        /// <summary>
        /// Update a Route
        /// </summary>
        /// <param name="route" type="Route">Route Model</param>
        /// <remarks>Update a Route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("EditRoute")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoute(Route route)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateRoute(route);

                _context.SaveRoute();
                return Ok(route.RouteId);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST: Map/DeleteRoute/5
        /// <summary>
        /// Delete a Route
        /// </summary>
        /// <param name="routeId" type="int">Route Id</param>
        /// <remarks>Delete a Route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeleteRoute")]
        [HttpPut]
        public IActionResult DeleteRoute(int routeId)
        {
            if (ModelState.IsValid)
            {
                
                Route route = _context.GetRouteByID(routeId);
                _context.DeleteRoute(route);
                _context.SaveRoute();
                return Ok(); 
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Debug.WriteLine("Errors found: " + errors + "\nEnd Errors found");
                return BadRequest(ModelState);
            }
        }
       
    }
}
