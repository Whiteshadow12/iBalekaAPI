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
    }
    public class ClubRepository : RepositoryBase<Club>, IClubRepository
    {
        private IClubMemberRepository _memberRepo;
        private IEventRepository _eventRepo;
        public ClubRepository(IDbFactory dbFactory,
            IClubMemberRepository memberRepo,
            IEventRepository eventRepo)
            : base(dbFactory)
        {
            _memberRepo = memberRepo;
            _eventRepo = eventRepo;
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
                    club.Event = _eventRepo.GetEvents().GetEventsByClubId(club.ClubId);
                    club.ClubMember = _memberRepo.GetClubMembersQuery().GetMembersByClubId(club.ClubId);
                }
            }
            return (ICollection<Club>)clubs;
        }
    }
}
