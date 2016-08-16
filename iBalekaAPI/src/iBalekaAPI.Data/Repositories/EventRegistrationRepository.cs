using Data.Extentions;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaAPI.Data.Repositories
{
    public interface IEventRegRepository : IRepository<EventRegistration>
    {
        EventRegistration GetEventRegByID(int id);
        IEnumerable<EventRegistration> GetAll(int eventId);
        IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId);
        void Register(EventRegistration reg);
        void DeRegister(int reg);
        //queries
        ICollection<EventRegistration> GetEventRegistrationsQuery();
    }
    public class EventRegistrationRepository : RepositoryBase<EventRegistration>, IEventRegRepository
    {
        private IEventRepository _eventRepo;
        private IAthleteRepository _athleteRepo;
        public EventRegistrationRepository(IDbFactory dbFactory,
            IEventRepository eventRepo,
            IAthleteRepository athleteRepo)

            : base(dbFactory)
        {
            _eventRepo = eventRepo;
            _athleteRepo = athleteRepo;
        }

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
            Add(reg);
        }
        public void DeRegister(int regId)
        {
            EventRegistration entity = GetEventRegByID(regId);
            entity.EventStatus = RegistrationType.Deregistered;
            Update(entity);
        }
        public override void Delete(EventRegistration entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        //queries
        public ICollection<EventRegistration> GetEventRegistrationsQuery()
        {
            IEnumerable<EventRegistration> evntRegs;
             evntRegs= DbContext.EventRegistration
                            .Where(p => p.Deleted == false
                                   && p.EventStatus != RegistrationType.Deregistered
                                   && p.EventStatus != RegistrationType.Closed)
                            .AsEnumerable();
            if (evntRegs.Count()>0)
            {
                foreach (EventRegistration reg in evntRegs)
                {
                    reg.Athlete = _athleteRepo.GetAthletesQuery().GetAthleteByAthleteId(reg.AthleteId);
                    reg.Event = _eventRepo.GetEventsQuery().GetEventByEventId(reg.EventId);
                } 
            }
            return (ICollection<EventRegistration>)evntRegs;
        }
    }
}
