using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Microsoft.AspNetCore.Identity;
using Data.Extentions;
using iBalekaAPI.Data.Configurations;

namespace iBalekaAPI.Data.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Route GetRouteByID(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        IEnumerable<Route> GetUserRoutes(string UserID);
        IEnumerable<Route> GetRoutes();
        void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints);
        Route AddRoute(Route route);
        Route UpdateRoute(Route route);
        void DeleteRoute(int routeId);
        ICollection<Route> GetRoutesQuery();
        Route GetRouteQuery(int routeId);
    }
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        private iBalekaDBContext DbContext;
        private IRunRepository runRepo;
        private IEventRepository eventRepo;
        public RouteRepository(iBalekaDBContext dbContext, IRunRepository _runRepo, IEventRepository _eventRepo)
            : base(dbContext)
        {
            runRepo = _runRepo;
            eventRepo = _eventRepo;
            DbContext = dbContext;
        }

        public Route AddRoute(Route route)
        {
            Route savingRoute = new Route();
            savingRoute.UserID = route.UserID;
            savingRoute.Title = route.Title;
            savingRoute.DateRecorded = route.DateRecorded;
            savingRoute.Distance = route.Distance;
            savingRoute.Location = route.Location;
            foreach (Checkpoint chps in route.Checkpoint)
            {
                Checkpoint checks = new Checkpoint(chps.Latitude, chps.Longitude);
                DbContext.Checkpoint.Add(checks);
                savingRoute.Checkpoint.Add(checks);
            }

            //create map image?
            DbContext.Route.Add(savingRoute);
            DbContext.SaveChanges();
            return savingRoute;
        }
        public Route UpdateRoute(Route updatedRoute)
        {
            IEnumerable<Checkpoint> checkpoints = GetCheckpoints(updatedRoute.RouteId);
            DeleteCheckPoints(checkpoints);
            Route route = GetRouteByID(updatedRoute.RouteId);
            route.Checkpoint = null;
            foreach (Checkpoint chp in updatedRoute.Checkpoint)
            {
                Checkpoint check = new Checkpoint(chp.Latitude, chp.Longitude);
                check.RouteId = updatedRoute.RouteId;
                DbContext.Checkpoint.Add(check);
                route.Checkpoint.Add(check);

            }
            route.Distance = updatedRoute.Distance;
            route.Title = updatedRoute.Title;
            route.Location = updatedRoute.Location;
            route.DateModified = updatedRoute.DateModified;

            DbContext.Route.Update(route);
            DbContext.SaveChanges();
            return route;
        }
        public Route GetRouteByID(int id)
        {
            return GetRouteQuery(id);
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return DbContext.Checkpoint.Where(x => x.RouteId == id).ToList();
        }
        public IEnumerable<Route> GetUserRoutes(string UserID)
        {
            ICollection<Route> routes;
            routes = DbContext.Route
                        .Where(r => r.Deleted == false && r.UserID == UserID)
                        .ToList();
            foreach (Route route in routes)
            {
                route.RunCount = runRepo.GetRouteRunCount(route.RouteId);
                route.EventCount = eventRepo.GetEventsByRoute(route.RouteId).Count();
            }
            return routes;
        }
        public IEnumerable<Route> GetRoutes()
        {
            return GetRoutesQuery();
        }
        public void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                DbContext.Entry(checkpoint).State = EntityState.Deleted;
            }
        }
        public void DeleteRoute(int routeId)
        {
            IEnumerable<Checkpoint> Checkpoints = GetCheckpoints(routeId);
            if (Checkpoints != null)
            {
                foreach (Checkpoint check in Checkpoints)
                {
                    check.Deleted = true;
                    DbContext.Entry(check).State = EntityState.Modified;
                }
            }
            Route deletedRoute = DbContext.Route.FirstOrDefault(x => x.RouteId == routeId);
            if (deletedRoute != null)
            {

                deletedRoute.Deleted = true;
                DbContext.Entry(deletedRoute).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
        }
        //queries
        public ICollection<Route> GetRoutesQuery()
        {
            ICollection<Route> routes;
            routes = DbContext.Route
                        .Where(r => r.Deleted == false)
                        .ToList();
            return routes;
        }
        public Route GetRouteQuery(int routeId)
        {
            Route routes;
            routes = DbContext.Route
                        .Where(r => r.Deleted == false && r.RouteId == routeId)
                        .Single();

            routes.Checkpoint = (ICollection<Checkpoint>)GetCheckpoints(routeId);

            return routes;
        }
    }
}

