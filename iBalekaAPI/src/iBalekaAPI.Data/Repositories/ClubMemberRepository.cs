using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaAPI.Data.Infastructure;
using iBalekaAPI.Models;
using Data.Extentions;

namespace iBalekaAPI.Data.Repositories
{
    public interface IClubMemberRepository : IRepository<ClubMember>
    {
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetMembers(int clubId);
        ICollection<ClubMember> GetClubMembersQuery();
    }
    public class ClubMemberRepository : RepositoryBase<ClubMember>, IClubMemberRepository
    {
        private IClubRepository _clubRepo;
        private IAthleteRepository _athleteRepo;
        public ClubMemberRepository(IDbFactory dbFactory,
            IClubRepository clubRepo,
            IAthleteRepository athleteRepo)
            : base(dbFactory)
        {
            _clubRepo = clubRepo;
            _athleteRepo = athleteRepo;
        }

        public ClubMember GetMemberByID(int id)
        {
            return GetClubMembersQuery().GetMembersById(id);
        }
        public IEnumerable<ClubMember> GetMembers(int clubId)
        {
            return GetClubMembersQuery().GetMembersByClubId(clubId);
        }
        public override void Add(ClubMember entity)
        {
            ClubMember exist = DbContext.ClubMember.Single(a => a.Club == entity.Club && a.AthleteId == entity.AthleteId);
            if (exist == null)
            {
                entity.DateJoined = DateTime.Now;
                entity.Status = ClubStatus.Joined;
                Add(entity);
            }
            else
            {
                exist.DateJoined = DateTime.Now;
                exist.Status = ClubStatus.Joined;
                Update(exist);
            }
        }
        public override void Delete(ClubMember entity)
        {
            entity.Status = ClubStatus.Left;
            entity.DateLeft = DateTime.Now;
            Update(entity);
        }

        public ICollection<ClubMember> GetClubMembersQuery()
        {
            IEnumerable<ClubMember> clubMembers;
            clubMembers = DbContext.ClubMember
                            .Where(p => p.Status == ClubStatus.Joined)
                            .AsEnumerable();
            if (clubMembers.Count() > 0)
            {
                foreach (ClubMember member in clubMembers)
                {
                    member.Club = _clubRepo.GetClubsQuery().GetClubByClubId(member.ClubId);
                    member.Athlete = _athleteRepo.GetAthletesQuery().GetAthleteByAthleteId(member.AthleteId);
                }
            }
            return (ICollection<ClubMember>)clubMembers;
        }
    }
}
