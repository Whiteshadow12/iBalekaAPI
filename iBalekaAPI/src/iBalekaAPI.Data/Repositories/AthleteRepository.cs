using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Data.Extentions;

namespace iBalekaAPI.Data.Repositories
{
    public interface IAthleteRepository:IRepository<Athlete>
    {
        Athlete GetAthleteByID(int id);
        IEnumerable<Athlete> GetAthletesQuery();
    }
    public class AthleteRepository:RepositoryBase<Athlete>,IAthleteRepository
    {
        private IClubMemberRepository _clubMemberRepo;
        private IEventRegRepository _evntRegRepo;
        private IRunRepository _runRepo;
        public AthleteRepository(IDbFactory dbFactory,
            IClubMemberRepository clubMemberRepo,
            IEventRegRepository evntRegRepo,
            IRunRepository runRepo)
            : base(dbFactory)
        {
            _clubMemberRepo = clubMemberRepo;
            _evntRegRepo = evntRegRepo;
            _runRepo = runRepo;
        }
       
        public Athlete GetAthleteByID(int athleteId)
        {
            return GetAthletesQuery().GetAthleteByAthleteId(athleteId);
        }
        public override IEnumerable<Athlete> GetAll()
        {
            return GetAthletesQuery();
        }
        public override void Delete(Athlete entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
        
        //queries
        public IEnumerable<Athlete> GetAthletesQuery()
        {
            IEnumerable<Athlete> athlete = DbContext.Athlete
                                .Where(p => p.Deleted ==false)
                                .AsEnumerable();
            if (athlete.Count()>0)
            {
                foreach (Athlete ath in athlete)
                {
                    ath.EventRegistration =_evntRegRepo.GetEventRegistrationsQuery().GetRegByAthleteId(ath.AthleteId);
                    ath.Run = _runRepo.GetRunsQuery().GetRunsByAthleteId(ath.AthleteId);
                } 
            }
            return athlete;
        }

    }
}
