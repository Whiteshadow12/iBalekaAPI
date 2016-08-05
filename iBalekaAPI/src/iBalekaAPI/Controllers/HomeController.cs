using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using iBalekaAPI.Services;
using Microsoft.AspNetCore.Identity;
using iBalekaAPI.Models;
using iBalekaAPI.Models.HomeViewModels;

namespace iBalekaAPI.Controllers
{

    public class HomeController : Controller
    {
        private IEventService _context;
        private IRouteService _routeContext;
        
        public HomeController(IEventService _repo,IRouteService _rContext)
        {
            _context = _repo;
            _routeContext = _rContext;
            
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult GetUserStats(string userId)
        {
            HomeViewModel model = new HomeViewModel();
            int nrEvents = int.Parse(_context.GetEvents(userId).Count().ToString());
            int nrRoutes = int.Parse(_routeContext.GetRoutes(userId).Count().ToString());
            model.NumberOfEvents = nrEvents;
            model.NumberOfRoutes = nrRoutes;
            return Json(model);
        }
        [HttpGet]
        public IActionResult GetAthleteStats(string userId)
        {
            HomeViewModel model = new HomeViewModel();
            int nrEvents = int.Parse(_context.GetEvents(userId).Count().ToString());
            int nrRoutes = int.Parse(_routeContext.GetRoutes(userId).Count().ToString());
            model.NumberOfEvents = nrEvents;
            model.NumberOfRoutes = nrRoutes;
            return Json(model);
        }
        


    }
}
