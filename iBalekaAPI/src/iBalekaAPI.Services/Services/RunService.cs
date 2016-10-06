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
        int GetRouteRunCount(int routeId);
        IEnumerable<Run> GetRouteRuns(int id);
        IEnumerable<Run> GetAllRuns(int id);
        Run AddRun(Run run);
        Run UpdateRun(Run run);
        void Delete(int run);
        void SaveRun();
        double GetTotalDistanceRan(int athleteId);
        double GetRunCount(int athleteId);
        double GetEventRunCount(int athleteId);
        double GetPersonalRunCount(int athleteId);
        double GetCaloriesOverTime(int athleteId, string startDate, string endDate);
        double GetDistanceOverTime(int athleteId, string startDate, string endDate);
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
        public int GetRouteRunCount(int routeId)
        {
            return _runRepo.GetRouteRunCount(routeId);
        }
        public double GetRunCount(int athleteId)
        {
            return _runRepo.GetRunCount(athleteId);
        }
        public double GetEventRunCount(int athleteId)
        {
            return _runRepo.GetEventRunCount(athleteId);
        }
        public double GetPersonalRunCount(int athleteId)
        {
            return _runRepo.GetPersonalRunCount(athleteId);
        }
        public double GetCaloriesOverTime(int athleteId, string startDate, string endDate)
        {
            return _runRepo.GetCaloriesOverTime(athleteId, startDate, endDate);
        }
        public double GetDistanceOverTime(int athleteId, string startDate, string endDate)
        {
            return _runRepo.GetDistanceOverTime(athleteId, startDate, endDate);
        }
        public double GetTotalDistanceRan(int athleteId)
        {
            return _runRepo.GetTotalDistanceRan(athleteId);
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
            return _runRepo.GetRouteRun(id);
        }
        public IEnumerable<Run> GetAthletePersonalRuns(int id)
        {
            return _runRepo.GetAthletePersonalRuns(id);
        }
        public IEnumerable<Run> GetAllRuns(int id)
        {
            return _runRepo.GetAllRuns(id);
        }
        public Run AddRun(Run run)
        {
            return _runRepo.AddRun(run);
        }
        public Run UpdateRun(Run run)
        {
            return _runRepo.UpdateRun(run);
        }
        public void Delete(int run)
        {
            _runRepo.Delete(run);
        }
        public void SaveRun()
        {
            unitOfWork.Commit();
        }
    }
}
