using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace iBalekaAPI.Services
{
    public interface IRouteService
    {
        Route GetRouteByID(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        IEnumerable<Route> GetUserRoutes(string UserID);
        IEnumerable<Route> GetRoutes();
        void AddRoute(Route route);
        void UpdateRoute(Route route);
        void DeleteRoute(Route route);
        void SaveRoute();
    }

    public class RouteService:IRouteService
    {
        private readonly IRouteRepository _routeRepo;
        private readonly IUnitOfWork unitOfWork;

        public RouteService(IRouteRepository _repo, IUnitOfWork _unitOfWork)
        {
            _routeRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return _routeRepo.GetCheckpoints(id);
        }
        public IEnumerable<Route> GetUserRoutes(string UserID)
        {
            return _routeRepo.GetUserRoutes(UserID);
        }
        public IEnumerable<Route> GetRoutes()
        {
            return _routeRepo.GetRoutes();
        }
        public Route GetRouteByID(int id)
        {
            return _routeRepo.GetRouteByID(id);
        }
        public Route GetRouteByIDView(int id)
        {
            return _routeRepo.GetRouteByIDView(id);
        }
        public void AddRoute(Route route)
        { 
            _routeRepo.AddRoute(route);
        }
        public void UpdateRoute(Route route)
        {
            _routeRepo.UpdateRoute(route);
        }
        public void DeleteRoute(Route route)
        {
            _routeRepo.Delete(route);
        }
        public void SaveRoute()
        {
            unitOfWork.Commit();
        }
    }
}

