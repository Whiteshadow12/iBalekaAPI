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
        [Route("GetUserRoutes")]
        [HttpGet]
        public IActionResult GetUserRoutes(string userId)
        {
            IEnumerable<Route> routes = _context.GetUserRoutes(userId);
            return Json(routes);
        }
        [Route("GetRoutes")]
        [HttpGet]
        public IActionResult GetRoutes(string userId)
        {
            IEnumerable<Route> routes = _context.GetRoutes();
            return Json(routes);
        }
        //// POST: Map/AddRoute
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
        [Route("GetRoute")]
        [HttpGet]
        public IActionResult GetRoute(int id)
        {
            Route route = _context.GetRouteByID(id);
            if (route == null)
            {
                return NotFound();
            }

            return Json(route);
        }

        // POST: Map/Edit/5
        [Route("EditRoute")]
        [HttpPost]
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
        [Route("DeleteRoute")]
        [HttpPost]
        public IActionResult DeleteRoute(int id)
        {
            if (ModelState.IsValid)
            {
                
                Route route = _context.GetRouteByID(id);
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
