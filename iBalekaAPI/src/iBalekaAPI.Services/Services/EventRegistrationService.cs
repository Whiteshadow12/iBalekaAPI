using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Models;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Data.Infastructure;


namespace iBalekaAPI.Services
{
    public interface IEventRegService
    {
        EventRegistration GetEventRegByID(int id);
        IEnumerable<EventRegistration> GetAll(int eventId);
        void Register(EventRegistration reg);
        IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId);
        void DeRegister(int reg);
        void SaveEventRegistration();
    }
    public class EventRegistrationService:IEventRegService
    {
        private readonly IEventRepository _eventRegistrationRepository;
        private readonly IUnitOfWork unitOfWork;

        public EventRegistrationService(IEventRepository _repo,IUnitOfWork _unitOfWork)
        {
            _eventRegistrationRepository = _repo;
            unitOfWork = _unitOfWork;
        }

        public EventRegistration GetEventRegByID(int id)
        {
            return _eventRegistrationRepository.GetEventRegByID(id);
        }
        public IEnumerable<EventRegistration> GetAthleteRegistrations(int athleteId)
        {
            return _eventRegistrationRepository.GetAthleteRegistrations(athleteId);
        }
        public IEnumerable<EventRegistration> GetAll(int eventId)
        {
            return _eventRegistrationRepository.GetAll(eventId);
        }
        public void Register(EventRegistration reg)
        {
            _eventRegistrationRepository.Register(reg);
        }
        public void DeRegister(int reg)
        {
            _eventRegistrationRepository.DeRegister(reg);
        }
        public void Delete(int evntReg)
        {
            _eventRegistrationRepository.DeleteEventReg(evntReg);
        }
        public void SaveEventRegistration()
        {
            unitOfWork.Commit();
        }
    }
}
