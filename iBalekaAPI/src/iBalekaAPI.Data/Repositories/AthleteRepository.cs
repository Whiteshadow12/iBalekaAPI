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
    public interface IAthleteRepository : IRepository<Athlete>
    {
        Athlete GetAthleteByID(int id);
        Athlete LoginAthlete(string email, string password);
        Athlete ChangePassword(Athlete athlete);
        Athlete AddAthlete(Athlete athlete);
        Athlete UpdateAthlete(Athlete athlete);
        void DeleteAthlete(int athlete);
        ICollection<Athlete> GetAthletesQuery();
        // void Delete(int entity);


    }
    public class AthleteRepository : RepositoryBase<Athlete>, IAthleteRepository
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
        public Athlete LoginAthlete(string username, string password)
        {
            Athlete loginAthlete = GetAthletesQuery()
                                    .Where(a => a.UserName == username
                                            && a.Password == password)
                                    .Single();

            return loginAthlete;

        }
        public Athlete ChangePassword(Athlete athlete)
        {
            DbContext.Athlete.Update(athlete);
            DbContext.SaveChanges();
            return athlete;
        }
        public Athlete AddAthlete(Athlete athlete)
        {
            Athlete savingAthlete = new Athlete()
            {
                UserName = athlete.UserName,
                Surname = athlete.Surname,
                Country = athlete.Country,
                Name = athlete.Name,
                DateOfBirth = athlete.DateOfBirth,
                EmailAddress = athlete.EmailAddress,
                Gender = athlete.Gender,
                Password = athlete.Password,
                SecurityQuestion = athlete.SecurityQuestion,
                SecurityAnswer = athlete.SecurityAnswer,
                Deleted = false,
                DateJoined = DateTime.Now.ToString()
            };
            DbContext.Athlete.Add(savingAthlete);

            DbContext.SaveChanges();
            Athlete newAthlete = GetAthletesQuery()
                                    .Where(a => a.Name == athlete.Name
                                            && a.EmailAddress == athlete.EmailAddress
                                            && a.Password == athlete.Password)
                                    .Single();
            return newAthlete;
        }
        public Athlete UpdateAthlete(Athlete athlete)
        {
            DbContext.Athlete.Update(athlete);
            DbContext.SaveChanges();
            return athlete;
        }
        public void DeleteAthlete(int athlete)
        {
            Athlete dlete = GetAthleteByID(athlete);
            dlete.Deleted = true;
            DbContext.SaveChanges();
        }
        public override IEnumerable<Athlete> GetAll()
        {
            return GetAthletesQuery().AsEnumerable();
        }
        public override void Delete(int entity)
        {
            Athlete athlete = GetAthleteByID(entity);
            athlete.Deleted = true;
            DbContext.SaveChanges();
        }

        //queries
        public ICollection<Athlete> GetAthletesQuery()
        {
            ICollection<Athlete> athlete = DbContext.Athlete
                                .Where(p => p.Deleted == false)
                                .ToList();

            return athlete;
        }

    }
}
