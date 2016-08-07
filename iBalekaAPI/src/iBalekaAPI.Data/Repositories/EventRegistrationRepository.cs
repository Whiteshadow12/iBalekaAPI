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
    }
    public class EventRegistrationRepository : RepositoryBase<EventRegistration>, IEventRegRepository
    {
        public EventRegistrationRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public EventRegistration GetEventRegByID(int id)
        {
            return DbContext.EventRegistration.Where(m => m.RegistrationId == id && m.Deleted == false).FirstOrDefault();
        }
        public IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId)
        {
            return DbContext.EventRegistration.Where(a => a.Deleted == false
                                                    && a.AthleteId == athleteId).ToList();
        }
        public IEnumerable<EventRegistration> GetAll(int eventId)
        {
            return DbContext.EventRegistration.Where(a => a.Deleted==false 
                                                    && a.EventStatus != RegistrationType.Deregistered 
                                                    && a.EventStatus != RegistrationType.Closed 
                                                    && a.EventId==eventId).ToList();
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
    }
}
