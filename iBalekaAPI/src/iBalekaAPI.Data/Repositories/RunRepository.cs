using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using iBalekaAPI.Data.Configurations;
using Data.Extentions;

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
        //queries
        ICollection<Run> GetRunsQuery();
        ICollection<Run> GetAthleteRunsQuery(int athleteId);
        Run AddRun(Run run);
        double GetTotalDistanceRan(int athleteId);
        double GetRunCount(int athleteId);
        double GetEventRunCount(int athleteId);
        double GetPersonalRunCount(int athleteId);
        double GetCaloriesOverTime(int athleteId, string startDate, string endDate);
        double GetDistanceOverTime(int athleteId, string startDate, string endDate);
    }
    public class RunRepository : RepositoryBase<Run>, IRunRepository
    {
        private iBalekaDBContext DbContext;
        public RunRepository(iBalekaDBContext dbContext)
            : base(dbContext)
        {
            DbContext = dbContext;
        }
        public Run GetRunByID(int id)
        {
            return GetRunsQuery().GetRunByRunId(id);
        }
        public double GetDistanceOverTime(int athleteId, string startDate, string endDate)
        {
            return GetAthleteRunsQuery(athleteId).GetDistanceOverTime(startDate, endDate);
        }
        public double GetCaloriesOverTime(int athleteId,string startDate, string endDate)
        {
            return GetAthleteRunsQuery(athleteId).GetCaloriesOverTime(startDate, endDate);
        }
        public double GetPersonalRunCount(int athleteId)
        {
            return GetAthleteRunsQuery(athleteId).GetPersonalRunCount();
        }
        public double GetEventRunCount(int athleteId)
        {
            return GetAthleteRunsQuery(athleteId).GetEventRunCount();
        }
        public double GetRunCount(int athleteId)
        {
            return GetAthleteRunsQuery(athleteId).GetRunCount();
        }
        public double GetTotalDistanceRan(int athleteId)
        {
            return GetAthleteRunsQuery(athleteId).GetTotalDistanceRan();
        }
        public IEnumerable<Run> GetAthleteEventRuns(int athleteId)
        {
            return GetAthleteRunsQuery(athleteId).GetRunsByAthleteEventRuns();
        }
        public IEnumerable<Run> GetAthletePersonalRuns(int athleteId)
        {
            return GetAthleteRunsQuery(athleteId).GetRunsByAthletePersonalRuns();
        }
        public IEnumerable<Run> GetEventRuns(int id)
        {
            return GetRunsQuery().GetRunsByEventId(id);
        }
        public IEnumerable<Run> GetRouteRuns(int id)
        {
            return GetRunsQuery().GetRunsByRouteId(id);
        }
        public IEnumerable<Run> GetAllRuns(int id)
        {
            return GetAthleteRunsQuery(id);
        }
        public override void Delete(int entity)
        {
            Run deletedRun = DbContext.Run.FirstOrDefault(x => x.RunId == entity);
            if (deletedRun != null)
            {
                deletedRun.Deleted = true;
                DbContext.Entry(deletedRun).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
        }
        public Run AddRun(Run run)
        {
            run.Deleted = false;
            run.DateRecorded = DateTime.Now;
            DbContext.Entry(run).State = EntityState.Added;
            DbContext.SaveChanges();
            Run newRun = GetAthleteRunsQuery(run.AthleteId)
                            .Where(a => a.DateRecorded == run.DateRecorded
                                    && a.StartTime == run.StartTime
                                    && a.EndTime == run.EndTime)
                             .Single();
            return run;
        }

        //queries
        public ICollection<Run> GetRunsQuery()
        {
            ICollection<Run> runs = DbContext.Run
                        .Where(p => p.Deleted == false)
                        .ToList();
            return runs;
        }
        public ICollection<Run> GetAthleteRunsQuery(int athleteId)
        {
            ICollection<Run> runs = DbContext.Run
                        .Where(p => p.Deleted == false
                                    && p.AthleteId == athleteId)
                        .ToList();
            return runs;
        }
    }
}
