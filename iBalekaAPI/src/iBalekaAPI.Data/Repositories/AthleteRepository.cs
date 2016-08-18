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
        public AthleteRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
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
            
            return athlete;
        }

    }
}
