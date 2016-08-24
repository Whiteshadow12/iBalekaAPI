using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Data.Extentions;
using iBalekaAPI.Data.Configurations;

namespace iBalekaAPI.Data.Repositories
{
    public interface IAthleteRepository:IRepository<Athlete>
    {
        Athlete GetAthleteByID(int id);
        ICollection<Athlete> GetAthletesQuery();
       // void Delete(int entity);


    }
    public class AthleteRepository:RepositoryBase<Athlete>,IAthleteRepository
    {
        private iBalekaDBContext DbContext;
        public AthleteRepository(iBalekaDBContext dbContext)
            : base(dbContext)
        {
            DbContext = dbContext;
        }
       
        public Athlete GetAthleteByID(int athleteId)
        {
            return GetAthletesQuery().GetAthleteByAthleteId(athleteId);
        }
        public override IEnumerable<Athlete> GetAll()
        {
            return GetAthletesQuery().AsEnumerable();
        }
        public override void Delete(int entity)
        {
            Athlete athlete = GetAthleteByID(entity);
            athlete.Deleted = true;
            
        }
        
        //queries
        public ICollection<Athlete> GetAthletesQuery()
        {
            ICollection<Athlete> athlete = DbContext.Athlete
                                .Where(p => p.Deleted ==false)
                                .ToList();
            
            return athlete;
        }

    }
}
