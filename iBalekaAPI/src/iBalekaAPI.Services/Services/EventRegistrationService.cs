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
        IEnumerable<EventRegistration> GetAll();
        void AddEventRegistration(EventRegistration evntReg);
        void UpdateEventRegistration(EventRegistration evntReg);
        void Delete(EventRegistration evntReg);
        void SaveEventRegistration();
    }
    public class EventRegistrationService:IEventRegService
    {
        private readonly IEventRegRepository _eventRegistrationRepository;
        private readonly IUnitOfWork unitOfWork;

        public EventRegistrationService(IEventRegRepository _repo,IUnitOfWork _unitOfWork)
        {
            _eventRegistrationRepository = _repo;
            unitOfWork = _unitOfWork;
        }

        public EventRegistration GetEventRegByID(int id)
        {
            return _eventRegistrationRepository.GetEventRegByID(id);
        }
        public IEnumerable<EventRegistration> GetAll()
        {
            return _eventRegistrationRepository.GetAll();
        }
        public void AddEventRegistration(EventRegistration evntReg)
        {
            _eventRegistrationRepository.Add(evntReg);
        }
        public void UpdateEventRegistration(EventRegistration evntReg)
        {
            _eventRegistrationRepository.Update(evntReg);
        }
        public void Delete(EventRegistration evntReg)
        {
            _eventRegistrationRepository.Delete(evntReg);
        }
        public void SaveEventRegistration()
        {
            unitOfWork.Commit();
        }
    }
}
