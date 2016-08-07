using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;

namespace iBalekaAPI.Data.Repositories
{
    public interface IRunRepository : IRepository<Run>
    {
        Run GetRunByID(int id);
        IEnumerable<Run> GetAthleteEventRuns(int id);
        IEnumerable<Run> GetAthletePersonalRuns(int id);
        IEnumerable<Run> GetEventRuns(int id);
        IEnumerable<Run> GetRouteRuns(int id);
        IEnumerable<Run> GetAllRuns(int id);
        void AddRun(Run run);
    }
    public class RunRepository : RepositoryBase<Run>, IRunRepository
    {
        public RunRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public Run GetRunByID(int id)
        {
            return DbContext.Run.Single(a => a.RunId == id && a.Deleted == false);
        }
        public IEnumerable<Run> GetAthleteEventRuns(int athleteId)
        {
            return DbContext.Run.Where(a => a.RunType==RunType.Event && a.AthleteId==athleteId && a.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetAthletePersonalRuns(int athleteId)
        {
            return DbContext.Run.Where(a => a.RunType == RunType.Personal && a.AthleteId == athleteId && a.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetEventRuns(int id)
        {
            return DbContext.Run.Where(a => a.EventId == id && a.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetRouteRuns(int id)
        {
            return DbContext.Run.Where(a =>a.RouteId == id && a.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetAllRuns(int id)
        {
            return DbContext.Run.Where(a => a.AthleteId == id && a.Deleted == false).ToList();
        }
        public override void Delete(Run entity)
        {
            Run deletedRun = DbContext.Run.FirstOrDefault(x => x.RunId == entity.RunId);
            if (deletedRun != null)
            {
                deletedRun.Deleted = true;
                DbContext.Entry(deletedRun).State = EntityState.Modified;
            }
        }
        public void AddRun(Run run)
        {
            run.Deleted = false;
            run.DateRecorded = DateTime.Now;
            DbContext.Entry(run).State = EntityState.Added;
        }

    }
}
