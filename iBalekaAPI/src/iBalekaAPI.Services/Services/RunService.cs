using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Data.Repositories;
using iBalekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaAPI.Services
{
    public interface IRunService
    {
        Run GetRunByID(int id);
        IEnumerable<Run> GetAthleteEventRuns(int id);
        IEnumerable<Run> GetAthletePersonalRuns(int id);
        IEnumerable<Run> GetEventRuns(int id);
        IEnumerable<Run> GetRouteRuns(int id);
        IEnumerable<Run> GetAllRuns(int id);
        void AddRun(Run run);
        void UpdateRun(Run run);
        void Delete(Run run);
        void SaveRun();
    }
    public class RunService:IRunService
    {
        private readonly IRunRepository _runRepo;
        private readonly IUnitOfWork unitOfWork;

        public RunService(IRunRepository _repo,IUnitOfWork _unitOfWork)
        {
            _runRepo = _repo;
            unitOfWork = _unitOfWork;
        }

        public Run GetRunByID(int id)
        {
            return _runRepo.GetRunByID(id);
        }
        public IEnumerable<Run> GetEventRuns(int id)
        {
            return _runRepo.GetEventRuns(id);
        }
        public IEnumerable<Run> GetAthleteEventRuns(int id)
        {
            return _runRepo.GetAthleteEventRuns(id);
        }
        public IEnumerable<Run> GetRouteRuns(int id)
        {
            return _runRepo.GetRouteRuns(id);
        }
        public IEnumerable<Run> GetAthletePersonalRuns(int id)
        {
            return _runRepo.GetAthletePersonalRuns(id);
        }
        public IEnumerable<Run> GetAllRuns(int id)
        {
            return _runRepo.GetAllRuns(id);
        }
        public void AddRun(Run run)
        {
            _runRepo.Add(run);
        }
        public void UpdateRun(Run run)
        {
            _runRepo.Update(run);
        }
        public void Delete(Run run)
        {
            _runRepo.Delete(run);
        }
        public void SaveRun()
        {
            unitOfWork.Commit();
        }
    }
}
