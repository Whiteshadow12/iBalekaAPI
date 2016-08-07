using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

//using prototypeWeb.Models;

namespace iBalekaAPI.Controllers
{
    public class MapController : Controller
    {
        private IRouteService _context;
        public MapController(IRouteService _repo)
        {
            _context = _repo;
        }

        
        
        //// GET: Map/SavedRoutes
        [HttpGet(Name = "GetUserRoutes")]
        public IActionResult GetUserRoutes(string userId)
        {
            IEnumerable<Route> routes = _context.GetUserRoutes(userId);
            return Json(routes);
        }
        [HttpGet(Name = "GetRoutes")]
        public IActionResult GetRoutes(string userId)
        {
            IEnumerable<Route> routes = _context.GetRoutes();
            return Json(routes);
        }
        //// POST: Map/AddRoute
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
        [HttpGet(Name = "GetRoute")]
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
        [HttpPost(Name = "Edit")]
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
        [HttpPost, ActionName("Delete")]
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
