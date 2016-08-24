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
        void AddRoute(Route route);
        void UpdateRoute(Route route);
        void DeleteRoute(int routeId);
        ICollection<Route> GetRoutesQuery();
    }
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        private iBalekaDBContext DbContext;
        public RouteRepository(iBalekaDBContext dbContext)
            : base(dbContext)
        {
            DbContext = dbContext;
        }
        

        public void AddRoute(Route route)
        {
            Route savingRoute = new Route();
            savingRoute.UserID = route.UserID;
            savingRoute.Title = route.Title;
            savingRoute.Distance = route.Distance;
            foreach (Checkpoint chps in route.Checkpoint)
            {                
                Checkpoint checks = new Checkpoint(chps.Latitude, chps.Longitude);
                DbContext.Checkpoint.Add(checks);
                savingRoute.Checkpoint.Add(checks);
            }    
            
            
            //create map image?
            DbContext.Route.Add(savingRoute);
        }
        public void UpdateRoute(Route updatedRoute)
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
            route.DateModified = DateTime.Now.Date;
            
            DbContext.Route.Update(route);
        }
        public Route GetRouteByID(int id)
        {
            return GetRoutesQuery().GetRouteByRouteId(id);
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return DbContext.Checkpoint.Where(x => x.RouteId == id).ToList();
        }
        public IEnumerable<Route> GetUserRoutes(string UserID)
        {
            return GetRoutesQuery().GetRouteByUserId(UserID);
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

            }
        }
        //queries
        public ICollection<Route> GetRoutesQuery()
        {
            ICollection<Route> routes;
            routes = DbContext.Route
                        .Where(r => r.Deleted == false)
                        .ToList();
            if (routes.Count()>0)
            {
                foreach (Route rte in routes)
                {
                    rte.Checkpoint = (ICollection<Checkpoint>)GetCheckpoints(rte.RouteId);
                } 
            }
            return routes;
        }
    }
}

