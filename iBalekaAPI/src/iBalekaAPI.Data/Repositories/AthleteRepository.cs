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
        Athlete ForgotPassword(string email);
        Athlete RegisterAthlete(Athlete athlete);
        Athlete UpdateAthlete(Athlete athlete);
        void DeleteAthlete(int athlete);
        ICollection<Athlete> GetAthletesQuery();
        Athlete GetAthleteQuery(int athleteId);
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
            return GetAthleteQuery(athleteId);
        }
        public Athlete LoginAthlete(string username, string password)
        {
            int athleteId = DbContext.Athlete.Single(a => a.UserName == username
                                            && a.Password == password).AthleteId;
            Athlete loginAthlete = GetAthleteQuery(athleteId);
            return loginAthlete;

        }
        public Athlete ForgotPassword(string email)
        {
            Athlete athlete = DbContext.Athlete.Single(a => a.EmailAddress == email);
            return athlete;
        }
        public Athlete RegisterAthlete(Athlete athlete)
        {
            Athlete savingAthlete = new Athlete()
            {
                UserName = athlete.UserName,
                Surname = athlete.Surname,
                Country = athlete.Country,
                FirstName = athlete.FirstName,
                DateOfBirth = athlete.DateOfBirth,
                EmailAddress = athlete.EmailAddress,
                Gender = athlete.Gender,
                Password = athlete.Password,
                SecurityQuestion = athlete.SecurityQuestion,
                SecurityAnswer = athlete.SecurityAnswer,
                Deleted = false,
                DateJoined = athlete.DateJoined
            };
            DbContext.Athlete.Add(savingAthlete);

            DbContext.SaveChanges();
            Athlete newAthlete = GetAthletesQuery()
                                    .Where(a => a.FirstName == athlete.FirstName
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
        public Athlete GetAthleteQuery(int athleteId)
        {
            Athlete existingAthlete = DbContext.Athlete.Single(a => a.AthleteId == athleteId && a.Deleted == false);
            existingAthlete.EventRegistration = DbContext.EventRegistration.Where(a => a.AthleteId == athleteId).ToList();
            existingAthlete.Run = DbContext.Run.Where(a => a.AthleteId == athleteId).ToList();
            existingAthlete.ClubMember = DbContext.ClubMember.Where(a => a.AthleteId == athleteId && a.Status == ClubStatus.Joined).ToList();
            return existingAthlete;
        }

    }
}
