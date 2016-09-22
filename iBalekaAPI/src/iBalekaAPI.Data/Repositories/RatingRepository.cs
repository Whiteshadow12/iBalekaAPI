//using iBalekaAPI.Data.Configurations;
//using iBalekaAPI.Data.Infastructure;
//using iBalekaAPI.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace iBalekaAPI.Data.Repositories
//{
//    public interface IRatingRepository : IRepository<Rating>
//    {
//        Rating GetRatingByID(int id);
//        Rating GetByRun(int id);
//    }
//    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
//    {
//        private iBalekaDBContext DbContext;
//        public RatingRepository(iBalekaDBContext dbContext)
//            : base(dbContext)
//        {
//            DbContext = dbContext;
//        }

//        public Rating GetRatingByID(int id)
//        {
//            return DbContext.Rating.Where(m => m.RatingId == id && m.Deleted == false).FirstOrDefault();
//        }
//        public override IEnumerable<Rating> GetAll()
//        {
//            return DbContext.Rating.Where(a => a.Deleted == false).ToList();
//        }
//        public Rating GetByRun(int id)
//        {
//            return DbContext.Rating.Where(m => m.RunId == id).FirstOrDefault();
//        }
//        public override void Delete(Rating entity)
//        {
//            entity.Deleted = true;
//            Update(entity);
//        }
//    }
//}
