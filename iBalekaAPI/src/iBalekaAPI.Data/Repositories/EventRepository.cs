using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Data.Extentions;

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
        //Queries
        IEnumerable<Event> GetEventsQuery();
        ICollection<EventRoute> GetEventRoutesQuery(int evntId);

        //event reg
        EventRegistration GetEventRegByID(int id);
        IEnumerable<EventRegistration> GetAll(int eventId);
        IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId);
        void Register(EventRegistration reg);
        void DeRegister(int reg);
        void DeleteEventReg(EventRegistration entity);
        //queries
        ICollection<EventRegistration> GetEventRegistrationsQuery();
    }


    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        private IRouteRepository _routeRepo;
        private IAthleteRepository _athleteRepo;
        public EventRepository(IDbFactory dbFactory, IRouteRepository repo,IAthleteRepository athleteRepo)
            : base(dbFactory)
        {
            _athleteRepo = athleteRepo;
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

            foreach (EventRoute evntRoute in evnt.EventRoute)
            {
                EventRoute route = new EventRoute(DateTime.Now.ToString());
                route.Title = evntRoute.Title;
                route.Description = evnt.Description; 
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
        public IEnumerable<EventRoute> GetEventRoutes(int evntId)
        {
            return GetEventRoutesQuery(evntId);
        }
        public Event GetEventByID(int id)
        {
            return GetEventsQuery().GetEventByEventId(id);
        }
        public IEnumerable<Event> GetUserEvents(string userId)
        {
            return GetEventsQuery().GetEventByUserId(userId);
        }
        public IEnumerable<Event> GetEvents()
        {
            return GetEventsQuery();
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

        //Queries
        public IEnumerable<Event> GetEventsQuery()
        {
            IEnumerable<Event> events;

                events = DbContext.Event
                                    .Where(p => p.Deleted == false && p.EventStatus == EventType.Open)
                                    .AsEnumerable();
                foreach (Event evnt in events)
                {
                    evnt.EventRoute = GetEventRoutesQuery(evnt.EventId);
                    evnt.EventRegistration = GetEventRegistrationsQuery().GetRegByEventId(evnt.EventId);
                }
                return events;           
        }        
        public ICollection<EventRoute> GetEventRoutesQuery(int evntId)
        {
            ICollection<EventRoute> evntRoutes;
            evntRoutes = DbContext.EventRoute
                             .Where(p => p.Deleted == false && p.EventID == evntId)
                             .ToList();
            if(evntRoutes.Count()>0)
            {
                foreach(EventRoute route in evntRoutes)
                {
                    route.Route = _routeRepo.GetRoutesQuery().GetRouteByRouteId(route.RouteID);
                }
            }
            return evntRoutes;

        }

        //event reg
        public EventRegistration GetEventRegByID(int regId)
        {
            return GetEventRegistrationsQuery().GetRegByRegId(regId);
        }
        public IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId)
        {
            return GetEventRegistrationsQuery().GetRegByAthleteId(athleteId);
        }
        public IEnumerable<EventRegistration> GetAll(int eventId)
        {
            return GetEventRegistrationsQuery().GetRegByEventId(eventId);
        }
        public void Register(EventRegistration reg)
        {
            reg.EventStatus = RegistrationType.Registered;
            reg.Deleted = false;
            DbContext.EventRegistration.Add(reg);
        }
        public void DeRegister(int regId)
        {
            EventRegistration entity = GetEventRegByID(regId);
            entity.EventStatus = RegistrationType.Deregistered;
            DbContext.EventRegistration.Update(entity);
        }
        public void DeleteEventReg(EventRegistration entity)
        {
            entity.Deleted = true;
           DbContext.EventRegistration.Update(entity);
        }

        //queries
        public ICollection<EventRegistration> GetEventRegistrationsQuery()
        {
            ICollection<EventRegistration> evntRegs;
            evntRegs = DbContext.EventRegistration
                           .Where(p => p.Deleted == false
                                  && p.EventStatus != RegistrationType.Deregistered
                                  && p.EventStatus != RegistrationType.Closed)
                           .ToList();
            if (evntRegs.Count() > 0)
            {
                foreach (EventRegistration reg in evntRegs)
                {
                    reg.Athlete = _athleteRepo.GetAthletesQuery().GetAthleteByAthleteId(reg.AthleteId);
                    reg.Event = GetEventsQuery().GetEventByEventId(reg.EventId);
                }
            }
            return evntRegs;
        }

    }
}
