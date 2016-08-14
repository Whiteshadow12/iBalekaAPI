using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Microsoft.AspNetCore.Identity;


namespace iBalekaAPI.Data.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Route GetRouteByID(int id);
        Route GetRouteByIDView(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        IEnumerable<Route> GetUserRoutes(string UserID);
        IEnumerable<Route> GetRoutes();
        void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints);
        void AddRoute(Route route);
        void UpdateRoute(Route route);
    }
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {

        public RouteRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
            
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
            Route route = DbContext.Route.Single(m => m.RouteId == id && m.Deleted == false);
            return route;
        }
        public Route GetRouteByIDView(int id)
        {
            Route route = GetRouteByID(id);
            List<Checkpoint> checks = GetCheckpoints(route.RouteId).ToList();
            List<Checkpoint> checkViews = new List<Checkpoint>();
            foreach (Checkpoint check in checks)
            {
                checkViews.Add(new Checkpoint(check.Latitude, check.Longitude));
            }
            Route viewRoute = new Route(route.RouteId, route.Title, route.UserID, route.Distance, checkViews,route.DateRecorded,route.DateModified);

            return viewRoute;
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return DbContext.Checkpoint.Where(x => x.RouteId == id).ToList();
        }
        public IEnumerable<Route> GetUserRoutes(string UserID)
        {
            return DbContext.Route.Where(a => a.UserID == UserID && a.Deleted == false).ToList();
        }
        public IEnumerable<Route> GetRoutes()
        {
            return DbContext.Route.Where(a =>a.Deleted == false).Include(a=>a.Checkpoint).ToList();
        }
        public void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                DbContext.Entry(checkpoint).State = EntityState.Deleted;
            }
        }
        public override void Delete(Route entity)
        {
            IEnumerable<Checkpoint> Checkpoints = GetCheckpoints(entity.RouteId);
            if (Checkpoints != null)
            {
                foreach (Checkpoint check in Checkpoints)
                {
                    check.Deleted = true;
                    DbContext.Entry(check).State = EntityState.Modified;
                }
            }
            Route deletedRoute = DbContext.Route.FirstOrDefault(x => x.RouteId == entity.RouteId);
            if (deletedRoute != null)
            {

                deletedRoute.Deleted = true;
                DbContext.Entry(deletedRoute).State = EntityState.Modified;

            }
        }
    }
}

