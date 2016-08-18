using Data.Extentions;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaAPI.Data.Repositories
{
    public interface IClubRepository : IRepository<Club>
    {
        Club GetClubByID(int id);
        IEnumerable<Club> GetUserClubs(string userId);
        ICollection<Club> GetClubsQuery();
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetMembers(int clubId);
        void JoinClub(ClubMember entity);
        void LeaveClub(ClubMember entity);

        ICollection<ClubMember> GetClubMembersQuery();
    }
    public class ClubRepository : RepositoryBase<Club>, IClubRepository
    {
        private IAthleteRepository _athleteRepo;
        public ClubRepository(IDbFactory dbFactory,
            IAthleteRepository athleteRepo)
            : base(dbFactory)
        {
            _athleteRepo = athleteRepo;
        }

        public Club GetClubByID(int id)
        {
            return GetClubsQuery().GetClubByClubId(id);
        }
        public IEnumerable<Club> GetUserClubs(string userId)
        {
            return GetClubsQuery().GetClubByUserId(userId);
        }
        public override IEnumerable<Club> GetAll()
        {
            return GetClubsQuery();
        }
        public override void Delete(Club entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        //query
        public ICollection<Club> GetClubsQuery()
        {
            IEnumerable<Club> clubs;
            clubs = DbContext.Club
                    .Where(p => p.Deleted == false)
                    .AsEnumerable();
            if (clubs.Count() > 0)
            {
                foreach (Club club in clubs)
                {
                    club.ClubMember = GetClubMembersQuery().GetMembersByClubId(club.ClubId);
                }
            }
            return (ICollection<Club>)clubs;
        }

        //club members
        public ClubMember GetMemberByID(int id)
        {
            return GetClubMembersQuery().GetMembersById(id);
        }
        public IEnumerable<ClubMember> GetMembers(int clubId)
        {
            return GetClubMembersQuery().GetMembersByClubId(clubId);
        }
        public void JoinClub(ClubMember entity)
        {
            ClubMember exist = DbContext.ClubMember.Single(a => a.Club == entity.Club && a.AthleteId == entity.AthleteId);
            if (exist == null)
            {
                entity.DateJoined = DateTime.Now;
                entity.Status = ClubStatus.Joined;
                DbContext.ClubMember.Add(entity);
            }
            else
            {
                exist.DateJoined = DateTime.Now;
                exist.Status = ClubStatus.Joined;
               DbContext.ClubMember.Update(exist);
            }
        }
        public void LeaveClub(ClubMember entity)
        {
            entity.Status = ClubStatus.Left;
            entity.DateLeft = DateTime.Now;
            DbContext.ClubMember.Update(entity);
        }

        public ICollection<ClubMember> GetClubMembersQuery()
        {
            ICollection<ClubMember> clubMembers;
            clubMembers = DbContext.ClubMember
                            .Where(p => p.Status == ClubStatus.Joined)
                            .ToList();
            if (clubMembers.Count() > 0)
            {
                foreach (ClubMember member in clubMembers)
                {
                    member.Club = GetClubsQuery().GetClubByClubId(member.ClubId);
                    member.Athlete = _athleteRepo.GetAthletesQuery().GetAthleteByAthleteId(member.AthleteId);
                }
            }
            return clubMembers;
        }
    }
}
