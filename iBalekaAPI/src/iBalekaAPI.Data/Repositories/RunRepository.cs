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
        void AddRun(Run run);
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
        public IEnumerable<Run> GetAthleteEventRuns(int athleteId)
        {
            return GetRunsQuery().GetRunsByAthleteEventRuns(athleteId);
        }
        public IEnumerable<Run> GetAthletePersonalRuns(int athleteId)
        {
            return GetRunsQuery().GetRunsByAthletePersonalRuns(athleteId);
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
            return GetRunsQuery();
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

        //queries
        public ICollection<Run> GetRunsQuery()
        {
            ICollection<Run> runs = DbContext.Run
                        .Where(p => p.Deleted == false)
                        .ToList();
            return runs;
        }
    }
}
