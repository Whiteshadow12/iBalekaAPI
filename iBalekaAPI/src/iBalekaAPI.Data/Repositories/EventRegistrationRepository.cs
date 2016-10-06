using Data.Extentions;
using iBalekaAPI.Data.Configurations;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaAPI.Data.Repositories
{
    public interface IEventRegistrationRepository
    {
        EventRegistration GetEventRegByID(int id);
        IEnumerable<EventRegistration> GetEventRegByRoute(int routeId);
        IEnumerable<EventRegistration> GetAll(int eventId);
        IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId);
        EventRegistration Register(EventRegistration reg);
        void DeRegister(int reg);
        void DeleteEventReg(int entity);
    }
    public class EventRegistrationRepository:IEventRegistrationRepository
    {
        private iBalekaDBContext DbContext;
        private IAthleteRepository _athleteRepo;
        public EventRegistrationRepository(iBalekaDBContext dbContext, IAthleteRepository athleteRepo)
        {
            DbContext = dbContext;
            _athleteRepo = athleteRepo;
        }
        //event reg
        public EventRegistration GetEventRegByID(int regId)
        {
            EventRegistration evntRegs = DbContext.EventRegistration
                          .Where(p => p.Deleted == false
                                 && p.RegistrationId == regId)
                                 .Single();
            return evntRegs;
        }
        public IEnumerable<EventRegistration> GetEventRegByRoute(int routeId)
        {
            return DbContext.EventRegistration
                                .Where(p => p.SelectedRoute == routeId)
                                .ToList();
        }
        public IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId)
        {
            ICollection<EventRegistration> evntRegs;
            evntRegs = DbContext.EventRegistration
                           .Where(p => p.Deleted == false
                                  && p.EventStatus != RegistrationType.Deregistered
                                  && p.EventStatus != RegistrationType.Closed
                                  && p.AthleteId == athleteId)
                           .ToList();
            return evntRegs;
        }
        public IEnumerable<EventRegistration> GetAll(int eventId)
        {
            ICollection<EventRegistration> evntRegs;
            evntRegs = DbContext.EventRegistration
                           .Where(p => p.Deleted == false
                                  && p.EventStatus != RegistrationType.Deregistered
                                  && p.EventId == eventId)
                           .ToList();
            if (evntRegs.Count() > 0)
            {
                foreach (EventRegistration reg in evntRegs)
                {
                    reg.Athlete = _athleteRepo.GetAthletesQuery().GetAthleteByAthleteId(reg.AthleteId);
                }
            }
            return evntRegs;
        }
        public EventRegistration Register(EventRegistration reg)
        {
            reg.EventStatus = RegistrationType.Registered;
            reg.Deleted = false;
            DbContext.EventRegistration.Add(reg);
            DbContext.SaveChanges();
            return reg;
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
    }
}
