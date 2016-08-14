using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace iBalekaAPI.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        void AddEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute);
        Event GetEventByID(int id);
        IEnumerable<EventRoute> GetEventRoutes(int id);
        IEnumerable<Event> GetUserEvents(string userId);
        IEnumerable<Event> GetEvents();
    }
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        private IRouteRepository _routeRepo;
        public EventRepository(IDbFactory dbFactory, IRouteRepository repo)
            : base(dbFactory)
        {
            _routeRepo = repo;
        }
        //add addEvent
        public void AddEvent(Event evnt)
        {
            Event newEvent = new Event();
            newEvent.Title = evnt.Title;
            newEvent.Description = evnt.Description;
            newEvent.Date = evnt.Date;
            newEvent.Time = evnt.Time;
            newEvent.Location = evnt.Location;
            newEvent.UserID = evnt.UserID;


            //Event savedEvent = DbContext.Event.Single(x => x.UserID == newEvent.UserID && x.Title == newEvent.Title && x.DateCreated == newEvent.DateCreated && x.Deleted == false);
            foreach (EventRoute evntRoute in evnt.EventRoute)
            {
                EventRoute route = new EventRoute(DateTime.Now.ToString());
                //DbContext.EventRoute.Add(route);
                route.Title = evntRoute.Title;
                route.Description = evnt.Description; //evntRoute.Description;
                route.RouteID = evntRoute.RouteID;
                newEvent.EventRoute.Add(route);
            }
            DbContext.Event.Add(newEvent);
            DbContext.SaveChanges();
        }

        public void UpdateEvent(Event evnt)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evnt.EventId);
            Event newEvent = GetEventByID(evnt.EventId);
            newEvent.Title = evnt.Title;
            newEvent.Description = evnt.Description;
            newEvent.Date = evnt.Date;
            newEvent.Time = evnt.Time;
            newEvent.Location = evnt.Location;
            newEvent.DateModified = DateTime.Now.ToString();


            //DeleteEventRoutes(evntRoutes);
            foreach (EventRoute route in evntRoutes)
            {
                DbContext.Entry(route).State = EntityState.Deleted;
            }
            DbContext.SaveChanges();
            newEvent.EventRoute = new List<EventRoute>();
            foreach (EventRoute evntRoute in evnt.EventRoute)
            {
                EventRoute route = new EventRoute(evntRoute.DateAdded);
                route.Title = evntRoute.Title;
                route.Description = evnt.Description;
                route.RouteID = evntRoute.RouteID;
                newEvent.EventRoute.Add(route);
            }


            DbContext.Event.Update(newEvent);
        }
        public void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute)
        {
            foreach (EventRoute route in evntRoute)
            {
                route.Deleted = true;
                DbContext.Entry(route).State = EntityState.Modified;
            }
        }
        public void DeleteEventRoute(EventRoute evntRoute)
        {

            evntRoute.Deleted = true;
            DbContext.Entry(evntRoute).State = EntityState.Modified;

        }
        public IEnumerable<EventRoute> GetEventRoutes(int id)
        {
            return DbContext.EventRoute.Where(m => m.EventID == id && m.Deleted == false).ToList();
        }
        public Event GetEventByID(int id)
        {
            return DbContext.Event.Single(m => m.EventId == id && m.Deleted == false);
        }
        public IEnumerable<Event> GetUserEvents(string id)
        {
            return DbContext.Event.Where(a => a.Deleted == false && a.UserID == id);
        }
        public IEnumerable<Event> GetEvents()
        {
            //load events
            IEnumerable<Event> Events;
            Events = DbContext.Event
                .Where(a => a.Deleted == false && a.EventStatus == EventType.Open)
                .Include(a=>a.EventRoute)
                .ToList();
            //load event routes
            //Events
            //foreach (Event evnt in Events)
            //{
            //    evnt.EventRoute = 
            //}
            return Events;
        }
        public override void Delete(Event evnt)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evnt.EventId);
            if (evntRoutes != null)
            {
                foreach (EventRoute route in evnt.EventRoute)
                {
                    route.Deleted = true;
                    DbContext.Entry(route).State = EntityState.Modified;
                }
            }
            Event deletedEvent = DbContext.Event.Single(x => x.EventId == evnt.EventId);
            if (deletedEvent != null)
            {
                deletedEvent.Deleted = true;
                DbContext.Entry(deletedEvent).State = EntityState.Modified;
            }
        }
    }
}
