using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iBalekaAPI.Models;
using iBalekaAPI.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System;
using iBalekaAPI.Models.Responses;
using System.Threading.Tasks;
using iBalekaAPI.Core.Extensions;

namespace iBalekaAPI.Core.Controllers
{
    [Route("[controller]")]
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
        [HttpGet]
        [Route("User/[action]")]
        public async Task<IActionResult> GetUserRoutes([FromQuery]string userId)
        {
            var response = new ListModelResponse<Route>()
               as IListModelResponse<Route>;
            try
            {
                if (userId == null)
                    throw new Exception("User Id is null");
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Route> routes = _context.GetUserRoutes(userId);
                    if (routes == null)
                        throw new Exception("There are no routes");
                    return routes;
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
        /// Get all Routes
        /// </summary>
        /// <remarks>Get all Routes</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetRoutes()
        {
            var response = new ListModelResponse<Route>()
               as IListModelResponse<Route>;
            try
            {
                response.Model = await Task.Run(() =>
                {
                    IEnumerable<Route> routes = _context.GetRoutes();
                    if (routes == null)
                        throw new Exception("There are no routes");
                    return routes;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
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
        [HttpPost]
        [Route("[action]")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoute([FromBody]Route route, [FromQuery]string userId)
        {
            var response = new SingleModelResponse<Route>()
               as ISingleModelResponse<Route>;
            try
            {
                if (route == null && userId==null)
                    throw new Exception("Your whole request is messed up. Route and UserId missing");
                else if(route == null)
                    throw new Exception("Route is missing");
                else if(userId==null)
                    throw new Exception("User ID is missing");
                response.Model = await Task.Run(() =>
                {
                    route.UserID = userId;
                   Route newRoute = _context.AddRoute(route);
                   

                    return newRoute;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }


        // GET: Map/Edit/5
        /// <summary>
        /// Get a Route
        /// </summary>
        /// <param name="routeId" type="int">Route Id</param>
        /// <remarks>Get a Route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetRoute([FromQuery]int routeId)
        {
            var response = new SingleModelResponse<Route>()
               as ISingleModelResponse<Route>;
            try
            {
                if (routeId <1)
                    throw new Exception("Route Id is missing");
                response.Model = await Task.Run(() =>
                {
                    Route route = _context.GetRouteByID(routeId);
                    if (route == null)
                        throw new Exception("Route does not exist");
                    return route;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // POST: Map/Edit/5
        /// <summary>
        /// Update a Route
        /// </summary>
        /// <param name="route" type="Route">Route Model</param>
        /// <remarks>Update a Route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("Update/[action]")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoute([FromBody]Route route)
        {
            var response = new SingleModelResponse<Route>()
               as ISingleModelResponse<Route>;
            try
            {
                if (route == null)
                    throw new Exception("Route Model is missing");
                response.Model = await Task.Run(() =>
                {
                    Route uproute =_context.UpdateRoute(route);
                    return uproute;
                });
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // POST: Map/DeleteRoute/5
        /// <summary>
        /// Delete a Route
        /// </summary>
        /// <param name="routeId" type="int">Route Id</param>
        /// <remarks>Delete a Route</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("Delete/[action]")]
        public async Task<IActionResult> DeleteRoute([FromQuery]int routeId)
        {
            var response = new SingleModelResponse<Route>()
               as ISingleModelResponse<Route>;
            try
            {
                if (routeId.ToString() ==null)
                    throw new Exception("Route Model is missing");
                response.Model = await Task.Run(() =>
                {
                    _context.DeleteRoute(routeId);
                    Route route = new Route();
                    route.RouteId = routeId;
                    return route;
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
