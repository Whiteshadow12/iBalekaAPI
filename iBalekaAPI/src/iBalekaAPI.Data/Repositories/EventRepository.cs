using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Data.Extentions;
using iBalekaAPI.Data.Configurations;

namespace iBalekaAPI.Data.Repositories
{
    public interface IEventRepository
    {
        Event AddEvent(Event evnt);
        Event UpdateEvent(Event evnt);
        void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute);
        Event GetEventByID(int id);
        IEnumerable<EventRoute> GetEventRoutes(int id);
        IEnumerable<Event> GetUserEvents(string userId);
        IEnumerable<Event> GetEvents();
        void DeleteEvent(int evntId);
        //Queries
        ICollection<Event> GetEventsQuery();
        Event GetEventQuery(int eventId);
        ICollection<EventRoute> GetEventRoutesQuery(int evntId);

        //event reg
        EventRegistration GetEventRegByID(int id);
        IEnumerable<EventRegistration> GetAll(int eventId);
        IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId);
        EventRegistration Register(EventRegistration reg);
        void DeRegister(int reg);
        void DeleteEventReg(int entity);
        //queries
        ICollection<EventRegistration> GetEventRegistrationsQuery();

    }


    public class EventRepository : IEventRepository
    {
        private IRouteRepository _routeRepo;
        private IAthleteRepository _athleteRepo;
        private IClubRepository _clubRepo;
        private iBalekaDBContext DbContext;
        public EventRepository(iBalekaDBContext dbContext, IRouteRepository repo, IAthleteRepository athleteRepo, IClubRepository clubRep)
        {
            DbContext = dbContext;
            _athleteRepo = athleteRepo;
            _routeRepo = repo;
            _clubRepo = clubRep;
        }
        //add addEvent
        public Event AddEvent(Event evnt)
        {
            Event newEvent = new Event();
            newEvent.Title = evnt.Title;
            newEvent.Description = evnt.Description;
            newEvent.Date = evnt.Date;
            newEvent.Time = evnt.Time;
            newEvent.Location = evnt.Location;
            newEvent.UserID = evnt.UserID;
            newEvent.ClubID = evnt.ClubID;

            foreach (EventRoute evntRoute in evnt.EventRoute)
            {
                EventRoute route = new EventRoute(DateTime.Now.ToString());
                route.Title = evntRoute.Title;
                route.Description = evnt.Description;
                route.RouteID = evntRoute.RouteID;
                route.Distance = evntRoute.Distance;
                newEvent.EventRoute.Add(route);
            }
            DbContext.Event.Add(newEvent);
            DbContext.SaveChanges();
            Event retu = GetUserEvents(newEvent.UserID)
                            .Where(a => a.Title == newEvent.Title
                                    && a.Location == newEvent.Location
                                    && a.Description == newEvent.Description
                                    && a.DateCreated == newEvent.DateCreated)
                            .Single();
            return retu;
        }
        public Event UpdateEvent(Event evnt)
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
                route.Distance = evntRoute.Distance;
                newEvent.EventRoute.Add(route);
            }


            DbContext.Event.Update(newEvent);
            DbContext.SaveChanges();
            Event retu = GetEventByID(newEvent.EventId);
            return retu;
        }
        public void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute)
        {
            foreach (EventRoute route in evntRoute)
            {
                route.Deleted = true;
                DbContext.Entry(route).State = EntityState.Modified;
            }
            DbContext.SaveChanges();
        }
        public IEnumerable<EventRoute> GetEventRoutes(int evntId)
        {
            return GetEventRoutesQuery(evntId);
        }
        public Event GetEventByID(int id)
        {
            return GetEventQuery(id);
        }
        public IEnumerable<Event> GetUserEvents(string userId)
        {
            return GetEventsQuery().GetEventByUserId(userId);
        }
        public IEnumerable<Event> GetEvents()
        {
            return GetEventsQuery();
        }
        public void DeleteEvent(int evntId)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evntId);
            if (evntRoutes != null)
            {
                foreach (EventRoute route in evntRoutes)
                {
                    route.Deleted = true;
                    DbContext.Entry(route).State = EntityState.Modified;
                }
            }
            Event deletedEvent = DbContext.Event.Single(x => x.EventId == evntId);
            if (deletedEvent != null)
            {
                deletedEvent.Deleted = true;
                DbContext.Entry(deletedEvent).State = EntityState.Modified;
                DbContext.SaveChanges();
            }

        }

        //Queries
        public ICollection<Event> GetEventsQuery()
        {
            ICollection<Event> events;

            events = DbContext.Event
                                .Where(p => p.Deleted == false && p.EventStatus == EventType.Open)
                                .ToList();
            return events;
        }
        public Event GetEventQuery(int eventId)
        {
            Event events;
            events = DbContext.Event
                                    .Where(p => p.Deleted == false && p.EventId == eventId)
                                    .Single();
            events.EventRoute = GetEventRoutesQuery(eventId);
            if (events.ClubID != 0)
                events.Club = _clubRepo.GetClubByID(events.ClubID);
            return events;
        }
        public ICollection<EventRoute> GetEventRoutesQuery(int evntId)
        {

            List<EventRoute> evntRoutes = new List<EventRoute>();

            evntRoutes = DbContext.EventRoute
                                 .Where(p => p.Deleted == false && p.EventID == evntId)
                                 .ToList();
           
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
        public EventRegistration Register(EventRegistration reg)
        {
            reg.EventStatus = RegistrationType.Registered;
            reg.Deleted = false;
            DbContext.EventRegistration.Add(reg);
            DbContext.SaveChanges();
            return GetEventRegistrationsQuery()
                    .Where(a => a.DateRegistered == reg.DateRegistered
                            && a.AthleteId == reg.AthleteId
                            && a.EventId == reg.EventId)
                    .Single();
        }
        public void DeRegister(int regId)
        {
            EventRegistration entity = GetEventRegByID(regId);
            entity.EventStatus = RegistrationType.Deregistered;
            DbContext.EventRegistration.Update(entity);
            DbContext.SaveChanges();
        }
        public void DeleteEventReg(int entity)
        {
            EventRegistration reg = GetEventRegByID(entity);
            reg.Deleted = true;
            DbContext.EventRegistration.Update(reg);
            DbContext.SaveChanges();
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
